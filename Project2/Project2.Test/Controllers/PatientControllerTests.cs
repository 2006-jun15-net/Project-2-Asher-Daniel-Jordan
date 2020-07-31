using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Project2.API.Controllers;
using Project2.Domain.Interface;
using Project2.Domain.Model;
using Xunit;
using Project2.Domain.Service;

namespace Project2.Test.Controllers
{
    public class PatientControllerTests
    {
        //initial setup
        private readonly Mock<IPatientRepository> _mockRepo;
        private readonly Mock<IPatientRoomRepository> _roomMockRepo;
        private readonly PatientsController _controller;
        private readonly Mock<PatientService> _service;


        //initial setup
        public PatientControllerTests()
        {
            _mockRepo = new Mock<IPatientRepository>();
            _roomMockRepo = new Mock<IPatientRoomRepository>();
            _service = new Mock<PatientService>(_mockRepo.Object, _roomMockRepo.Object);
            _controller = new PatientsController(_mockRepo.Object, _service.Object);

            List<Patient> patients = new List<Patient>()
            {
                new Patient(1, 1, "Test", "Dummy"),
                new Patient(2, 4, "Test2", "Dummy2")
            };

            Nurse nurse = new Nurse(2, "Testy", "Dolly");

            List<Doctor> doctors = new List<Doctor>() {
                new Doctor(1, "Dr.Test", "Dummulus"),
                new Doctor(2, "Dr.Test2", "Dum")
            };

            List<WorkingDetails> workingDetails = new List<WorkingDetails>() {
                new WorkingDetails(2, 1, true),
                new WorkingDetails(3, 1, true)
            };

            List<Treatment> treatments = new List<Treatment>() {
                new Treatment(1, 2, 1, "TestDrug", 6),
                new Treatment(2, 5, 3, "Drug", 9)
            };

            List<TreatmentDetails> details = new List<TreatmentDetails>()
            {
                new TreatmentDetails(1, 4, 1, 1, "now"),
                new TreatmentDetails(2, 7, 3, 5, "then")
            };

            List<PatientRoom> rooms = new List<PatientRoom>()
            {
                new PatientRoom(1, true),
                new PatientRoom(2, true)
            };

            // get all patients
            _mockRepo.Setup(repo => repo.GetPatientsAsync())
                .Returns(async () => await Task.Run(() => patients));

            /*// get all patients by nurse
            _mockRepo.Setup(repo => repo.GetByNurseAsync(It.IsAny<int>()))
                .Returns(async (int id) => await Task.Run(() =>
                {
                    List<int> treatmentIds = new List<int>();
                    List<int> detailsIds = new List<int>();

                    var doctorIds= workingDetails.Where<WorkingDetails>(wd => wd.NurseId == id).Select(wd => wd.DoctorId).ToList();

                    foreach (var id in doctorIds)
                    {
                        treatmentIds.AddRange(treatments.Where(t => t.DoctorId == id).Select(t => t.TreatmentId).ToList());
                    }

                    foreach (var id in treatmentIds)
                    {
                        detailsIds.AddRange(details.Where(t => t.TreatmentId == id).Select(t => t.PatientId).ToList());
                    }

                    if(detailsIds.Count == 0)
                    {
                        return null;
                    }

                    return detailsIds;
                })*/
            // gets patient based on patient room
            _mockRepo.Setup(repo => repo.GetByPatientRoom(It.IsAny<int>()))
                .Returns(async (int id) => await Task.Run(() => 
                    patients.Where<Patient>(p => p.PatientRoomId == id).FirstOrDefault()));

            // get patient by id
            _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .Returns(async (int id) => await Task.Run(() =>
                    patients.Where<Patient>(p => p.PatientId == id).FirstOrDefault()));

            // create patient
            _mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<Patient>()))
                .Returns(async (Patient patient) => await Task.Run(() => patients.Add(patient)));

            // updates patient
            _mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Patient>()))
                .Returns(async (Patient patient) => await Task.Run(() => patients));

            // deletes a patient
            _mockRepo.Setup(repo => repo.DeletePatientAsync(It.IsAny<Patient>()))
                .Returns(async (Patient patient) => await Task.Run(() => patients.Remove(patient)));
        }


        [Fact]
        public async void Get_ActionExecutes_ReturnsOKStatus()
        {
            var result = await _controller.Get();

            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async void GetByPatientRoom_ActionExecutes_ReturnsOKStatus()
        {
            var result = await _controller.GetByPatientRoom(1);

            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async void GetByPatientRoom_Action_ReturnsNotFound()
        {
            var result = await _controller.GetByPatientRoom(23);

            var notFoundResult = result as NotFoundResult;

            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async void Get_Action_ReturnsOK_IfFound()
        {
            var result = await _controller.Get(1);

            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async void Get_Action_ReturnsNotFound()
        {
            var result = await _controller.Get(23);

            var notFoundResult = result as NotFoundResult;

            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async void Post_Action_ReturnsCreatedAtAction()
        {
            Patient patient = new Patient(3, 1, "Test", "Dummy");
            var result = await _controller.Post(patient);

            var createdResult = result as CreatedAtActionResult;

            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);
        }

        [Fact]
        public async void Post_Action_ReturnsConflict_OnDuplicate()
        {
            Patient patient = new Patient(1, 1, "Test", "Dummy");
            var result = await _controller.Post(patient);

            var createdResult = result as ConflictResult;

            Assert.NotNull(createdResult);
            Assert.Equal(409, createdResult.StatusCode);
        }

        [Fact]
        public async void Put_Action_ReturnsOk()
        {
            Patient patient = new Patient(1, 2, "Test", "Dummy");
            var result = await _controller.Put(1, patient);

            var okResult = result as OkResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async void Put_Action_ReturnsBadRequest()
        {
            Patient patient = new Patient(1, 2, "Test", "Dummy");
            var result = await _controller.Put(2, patient);

            var badRequestResult = result as BadRequestResult;

            Assert.NotNull(badRequestResult);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async void Put_Action_ReturnsNotFound()
        {
            Patient patient = new Patient(23, 2, "Test", "Dummy");
            var result = await _controller.Put(23, patient);

            var notFoundResult = result as NotFoundResult;

            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async void Delete_Action_ReturnsOk()
        {
            var result = await _controller.Delete(1);

            var okResult = result as OkResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async void Delete_Action_ReturnsNotFound()
        {
            var result = await _controller.Delete(23);

            var notFoundResult = result as NotFoundResult;

            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
        }
    }
}
