using Microsoft.EntityFrameworkCore;
using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Application.Interfaces.Repositories;
using SportsBicycleStore.Domain.Entities;
using SportsBicycleStore.Domain.Enum;
using SportsBicycleStore.Infastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Infastructure.Repositories
{
    public class MInspectionReportRepository : Repository<Minspectionreport> , IMInspectionReportRepository
    {
        public MInspectionReportRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Minspectionreport> CreateInspectionReport(InspectionReportDto inspectionReportDto)
        {
            var inspectionReport = new Minspectionreport
            {
                ReportId = Guid.NewGuid().ToString(),
                ProductId = inspectionReportDto.ProductId,
                InspectorId = inspectionReportDto.InspectorId,
                //InspectionDate = DateTime.UtcNow,
                ImagesUrl = inspectionReportDto.ImagesUrl,
                Status = (int)InspectionReportStatus.Pending,
                //CreatedAt = DateTime.UtcNow
            };

            await _context.Minspectionreports.AddAsync(inspectionReport);
            await _context.SaveChangesAsync();
            return inspectionReport;
        }

        public async Task<Minspectionreport?> GetInpectionReportByIdAsync(string reportId)
        {
            var report =  await _context.Minspectionreports.FirstOrDefaultAsync(r => r.ReportId == reportId);
            return report;
        }

        public async Task<bool> UpdateInspectionReport(Minspectionreport minspectionreport)
        {
            
            _context.Minspectionreports.Update(minspectionreport);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
