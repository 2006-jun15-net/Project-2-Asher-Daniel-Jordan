﻿using Project2.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Domain.Interface
{
    public interface ITreatmentRepository
    {
        Task<IEnumerable<Treatment>> GetTreatmentsAsync();
        Task<IEnumerable<Treatment>> GetDoctorTreatmentsAsync(int id);
        Task<IEnumerable<Treatment>> TreatmentsByIlllnessAsync(int doctorId, int illnessId);
        Task<Treatment> GetTreatmentAsync(int id);

        Task CreateTreatmentAsync(Treatment t);

        Task UpdateTreatmentAsync(Treatment t);

        Task DeleteTreatmentAsync(int id);
    }
}
