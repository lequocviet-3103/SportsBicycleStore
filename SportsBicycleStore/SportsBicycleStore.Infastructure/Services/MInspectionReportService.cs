using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Application.Interfaces.Repositories;
using SportsBicycleStore.Application.Interfaces.Services;
using SportsBicycleStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Infastructure.Services
{
    public class MInspectionReportService : IMInspectionReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MInspectionReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Minspectionreport> CreateInspectionReport(InspectionReportDto inspectionReportDto)
        {
            return await _unitOfWork.MInspectionReportRepository.CreateInspectionReport(inspectionReportDto);
        }

        public async Task<List<GetAllInspectorReport>> GetAllInspectionReportProductName()
        {
            return await _unitOfWork.MInspectionReportRepository.GetAllInspectionReportProductName();
        }

        public async Task<List<Minspectionreport>> GetAllInspectionReportsAsync()
        {
            return await _unitOfWork.MInspectionReportRepository.GetAllInspectionReportsAsync();
        }

        public async Task<GetAllInspectorReport?> GetInpectionReportByReportId(string reportId)
        {
            return await _unitOfWork.MInspectionReportRepository.GetInpectionReportByReportId(reportId);
        }

        public async Task<bool> UpdateInspectionReport(UpdateInspectionReportDto updateInspectionReportDto, string reportId)
        {
            var report = await _unitOfWork.MInspectionReportRepository.GetInpectionReportByIdAsync(reportId);
            if (report == null)
            {
                return false;
            }
            report.OverallRating = updateInspectionReportDto.OverallRating;
            report.FrameCondition = updateInspectionReportDto.FrameCondition;
            report.FrameNotes = updateInspectionReportDto.FrameNotes;
            report.BrakeCondition = updateInspectionReportDto.BrakeCondition;
            report.BrakeNotes = updateInspectionReportDto.BrakeNotes;
            report.DrivetrainCondition = updateInspectionReportDto.DrivetrainCondition;
            report.DrivetrainNotes = updateInspectionReportDto.DrivetrainNotes;
            report.WheelCondition = updateInspectionReportDto.WheelCondition;
            report.WheelNotes = updateInspectionReportDto.WheelNotes;
            report.Status = updateInspectionReportDto.Status;
            report.CompletedAt = DateTime.Now;
            await _unitOfWork.MInspectionReportRepository.UpdateInspectionReport(report);
            return true;

        }
    }
}
