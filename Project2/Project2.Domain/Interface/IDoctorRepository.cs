﻿using Project2.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project2.Domain.Interface
{
    public interface IDoctorRepository
    {
        IEnumerable<Doctor> GetAll();
        Doctor GetbyId(int id);
    }
}
