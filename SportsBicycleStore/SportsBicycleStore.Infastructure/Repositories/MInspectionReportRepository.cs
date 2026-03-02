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

        public async Task<List<GetAllInspectorReport>> GetAllInspectionReportProductName()
        {
            var inspectorReport = await _context.Minspectionreports.AsNoTracking()
                .Join(_context.Mproducts.AsNoTracking(),
                    report => report.ProductId,
                    product => product.ProductId,
                    (report, product) => new GetAllInspectorReport
                    {
                        ReportId = report.ReportId,
                        ProductId = report.ProductId,
                        ProductName = product.ProductName,
                        InspectorId = report.InspectorId,
                        InspectionDate = report.InspectionDate,
                        OverallRating = report.OverallRating,
                        FrameCondition = report.FrameCondition,
                        FrameNotes = report.FrameNotes,
                        BrakeCondition = report.BrakeCondition,
                        BrakeNotes = report.BrakeNotes,
                        DrivetrainCondition = report.DrivetrainCondition,
                        DrivetrainNotes = report.DrivetrainNotes,
                        WheelCondition = report.WheelCondition,
                        WheelNotes = report.WheelNotes,
                        ImagesUrl = report.ImagesUrl,
                        Status = report.Status,
                        CreatedAt = report.CreatedAt,
                        UpdatedAt = report.UpdatedAt,
                        CompletedAt = report.CompletedAt
                    })
                .ToListAsync();
            return inspectorReport;
        }

        public async Task<List<Minspectionreport>> GetAllInspectionReportsAsync()
        {
            return await _context.Minspectionreports.ToListAsync();
        }

        public async Task<Minspectionreport?> GetInpectionReportByIdAsync(string reportId)
        {
            var report =  await _context.Minspectionreports.FirstOrDefaultAsync(r => r.ReportId == reportId);
            return report;
        }

        public async Task<GetAllInspectorReport?> GetInpectionReportByReportId(string reportId)
        {
            var inspectorReport = await _context.Minspectionreports.AsNoTracking()
                .Join(_context.Mproducts.AsNoTracking(),
                    report => report.ProductId,
                    product => product.ProductId,
                    (report, product) => new GetAllInspectorReport
                    {
                        ReportId = report.ReportId,
                        ProductId = report.ProductId,
                        ProductName = product.ProductName,
                        InspectorId = report.InspectorId,
                        InspectionDate = report.InspectionDate,
                        OverallRating = report.OverallRating,
                        FrameCondition = report.FrameCondition,
                        FrameNotes = report.FrameNotes,
                        BrakeCondition = report.BrakeCondition,
                        BrakeNotes = report.BrakeNotes,
                        DrivetrainCondition = report.DrivetrainCondition,
                        DrivetrainNotes = report.DrivetrainNotes,
                        WheelCondition = report.WheelCondition,
                        WheelNotes = report.WheelNotes,
                        ImagesUrl = report.ImagesUrl,
                        Status = report.Status,
                        CreatedAt = report.CreatedAt,
                        UpdatedAt = report.UpdatedAt,
                        CompletedAt = report.CompletedAt
                    }).Where(r => r.ReportId == reportId)
                .FirstOrDefaultAsync();
            return inspectorReport;
        }

        public async Task<bool> UpdateInspectionReport(Minspectionreport minspectionreport)
        {
            
            _context.Minspectionreports.Update(minspectionreport);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
