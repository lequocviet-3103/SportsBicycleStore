using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Application.Interfaces.Services
{
    public interface IMInspectionReportService
    {
        public Task<Minspectionreport> CreateInspectionReport(InspectionReportDto inspectionReportDto);
        public Task<bool> UpdateInspectionReport(UpdateInspectionReportDto updateInspectionReportDto, string reportId);
    }
}
