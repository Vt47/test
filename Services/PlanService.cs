using System;
using System.Linq;
using System.Collections.Generic;
using smoking.Models;
using smoking.Data;

namespace smoking.Services
{
    public class PlanService
    {
        private readonly AppDbContext _context;

        public PlanService(AppDbContext context)
        {
            _context = context;
        }

        // 1. Tạo Plan và chia Phase
        public void CreatePlan(CreatePlanDto dto)
        {
            var plan = new Plan
            {
                Member_ID = dto.MemberId,
                QuitSmokingDate = dto.QuitSmokingDate,
                MaxCigarettes = dto.MaxCigarettes,
                TotalCigarettes = 0,
                SaveMoney = 0,
                CigarettesQuit = 0,
            };
            _context.Plan.Add(plan);
            _context.SaveChanges();

            int daysPerPhase = dto.GoalTime / 5;
            for (int i = 0; i < 5; i++)
            {
                var start = dto.QuitSmokingDate.AddDays(i * daysPerPhase);
                var end = (i == 4)
                    ? dto.QuitSmokingDate.AddDays(dto.GoalTime - 1)
                    : dto.QuitSmokingDate.AddDays((i + 1) * daysPerPhase - 1);

                var phase = new Phase
                {
                    Plan_ID = plan.Plan_ID,
                    PhaseNumber = i + 1,
                    StartDatePhase = start,
                    EndDatePhase = end,
                    StatusPhase = "Pending"
                };
                _context.Phase.Add(phase);
            }
            _context.SaveChanges();
        }

        // 2. Cập nhật số điếu hút mỗi ngày, tính toán chỉ số, kiểm tra phase
        public void UpdateTodayCigarettes(int planId, int todayCigarettes, DateTime date)
        {
            var plan = _context.Plan.FirstOrDefault(p => p.Plan_ID == planId);
            var member = _context.Member.FirstOrDefault(m => m.Member_ID == plan.Member_ID);

            var planDetail = _context.Plan_detail.FirstOrDefault(d => d.Plan_ID == planId && d.Date == date);
            if (planDetail == null)
            {
                planDetail = new Plan_detail { Plan_ID = planId, Date = date };
                _context.Plan_detail.Add(planDetail);
            }
            planDetail.TodayCigarettes = todayCigarettes;
            planDetail.MaxCigarettes = plan.MaxCigarettes;
            planDetail.IsSuccess = todayCigarettes <= plan.MaxCigarettes;

            int yesterdayCigarettesQuit = plan.CigarettesQuit;
            plan.CigarettesQuit = (plan.MaxCigarettes - todayCigarettes) + yesterdayCigarettesQuit;
            plan.SaveMoney = plan.CigarettesQuit * Convert.ToDecimal(member.CostPerCigarette);

            CheckPhaseStatus(planId, date);

            _context.SaveChanges();
        }

        // 3. Kiểm tra trạng thái phase
        public void CheckPhaseStatus(int planId, DateTime today)
        {
            var phases = _context.Phase.Where(p => p.Plan_ID == planId).ToList();
            var currentPhase = phases.FirstOrDefault(p => p.StartDatePhase <= today && p.EndDatePhase >= today);
            if (currentPhase == null) return;

            int phaseDays = (currentPhase.EndDatePhase - currentPhase.StartDatePhase).Days + 1;
            int maxFailDays = (int)Math.Floor(phaseDays * 0.2);

            int failDays = _context.Plan_detail
                .Where(d => d.Plan_ID == planId && d.Date >= currentPhase.StartDatePhase && d.Date <= currentPhase.EndDatePhase && d.IsSuccess == false)
                .Count();

            if (failDays > maxFailDays)
            {
                currentPhase.StatusPhase = "Fail";
                if (currentPhase.PhaseNumber < 5)
                {
                    var backupPhase = phases.FirstOrDefault(p => p.PhaseNumber == 5);
                    if (backupPhase != null)
                        backupPhase.StatusPhase = "Active";
                }
                else
                {
                    var plan = _context.Plan.FirstOrDefault(p => p.Plan_ID == planId);
                    // plan.Status = "Fail"; // Nếu có trường trạng thái
                }
            }
            else
            {
                currentPhase.StatusPhase = "Active";
            }
            _context.SaveChanges();
        }

        // 4. Lấy thông tin tổng hợp My Plan
        public MyPlanDto GetMyPlan(int accountId)
        {
            var member = _context.Member.FirstOrDefault(m => m.Account_ID == accountId);
            if (member == null)
                throw new Exception("Member not found");

            var plan = _context.Plan.FirstOrDefault(p => p.Member_ID == member.Member_ID);
            if (plan == null)
                throw new Exception("Plan not found");

            var today = DateTime.Today;

            int daysSinceStart = (today - plan.QuitSmokingDate).Days + 1;
            double cigarettesPerDay = Convert.ToDouble(member.CigarettesPerDay);
            int maxCigarettesToday = (int)Math.Round(cigarettesPerDay - (cigarettesPerDay / daysSinceStart));

            var todayDetail = _context.Plan_detail.FirstOrDefault(d => d.Plan_ID == plan.Plan_ID && d.Date == today);

            var phases = _context.Phase
                .Where(p => p.Plan_ID == plan.Plan_ID)
                .Select(p => new PhaseDto
                {
                    PhaseNumber = p.PhaseNumber,
                    StartDatePhase = p.StartDatePhase,
                    EndDatePhase = p.EndDatePhase,
                    StatusPhase = p.StatusPhase,
                    TotalDays = (p.EndDatePhase - p.StartDatePhase).Days + 1,
                    FailDays = _context.Plan_detail
                        .Where(d => d.Plan_ID == plan.Plan_ID && d.Date >= p.StartDatePhase && d.Date <= p.EndDatePhase && d.IsSuccess == false)
                        .Count()
                }).ToList();

            return new MyPlanDto
            {
                PlanId = plan.Plan_ID,
                QuitSmokingDate = plan.QuitSmokingDate,
                TotalCigarettes = plan.TotalCigarettes,
                MaxCigarettes = plan.MaxCigarettes,
                CigarettesQuit = plan.CigarettesQuit,
                SaveMoney = plan.SaveMoney,
                DaysSinceStart = daysSinceStart,
                MaxCigarettesToday = maxCigarettesToday,
                TodayCigarettes = todayDetail?.TodayCigarettes ?? 0,
                Phases = phases
            };
        }
    }
} 