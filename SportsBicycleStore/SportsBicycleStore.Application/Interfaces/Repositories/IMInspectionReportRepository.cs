using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Application.Interfaces.Repositories
{
    public interface IMInspectionReportRepository
    {
        public Task<Minspectionreport> CreateInspectionReport(InspectionReportDto inspectionReportDto);
        public Task<Minspectionreport?> GetInpectionReportByIdAsync(string reportId);
        public Task<bool> UpdateInspectionReport(Minspectionreport minspectionreport);
        public Task<List<Minspectionreport>> GetAllInspectionReportsAsync();
    }
}
