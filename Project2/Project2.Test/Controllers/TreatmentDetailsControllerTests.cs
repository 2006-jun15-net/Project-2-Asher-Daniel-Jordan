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

namespace Project2.Test.Controllers
{
    public class TreatmentDetailsControllerTests
    {
        //initial setup
        private readonly Mock<ITreatmentDetailsRepository> _mockRepo;
        private readonly TreatmentDetailsController _controller;


        //initial setup
        public TreatmentDetailsControllerTests()
        {
            _mockRepo = new Mock<ITreatmentDetailsRepository>();
            _controller = new TreatmentDetailsController(_mockRepo.Object);

            List<TreatmentDetails> details = new List<TreatmentDetails>()
            {
                new TreatmentDetails(1, 1, 1, 1, "7/28/2020 9:05:24 PM"),
                new TreatmentDetails(2, 4, 2, 5, "7/28/2020 9:08:43 PM")
            };

            List<Treatment> treatments = new List<Treatment>()
            {
                new Treatment(1, 3, 2, "TestDrug", 6),
                new Treatment(2, 6, 1, "TestPill", 3)
            };

            Doctor doctor = new Doctor(1, "Test", "Dummy");

            // get all treatmentDetails
            _mockRepo.Setup(repo => repo.GetAllAsync())
                .Returns(async () => await Task.Run(() => details));

            // get all patient treatmentDetails
            _mockRepo.Setup(repo => repo.GetPatientTreatment(It.IsAny<int>()))
                .Returns(async (int id) => await Task.Run(() => 
                    details.Where<TreatmentDetails>(td => td.PatientId == id)));

            // get all treatmentDetails by doctor
            _mockRepo.Setup(repo => repo.GetByDoctorAsync(It.IsAny<int>()))
                .Returns(async (int id) => await Task.Run(() =>
                {
                    var treatmentDetails = new List<TreatmentDetails>();
                    var treatmentIds = treatments.Where<Treatment>(t => t.DoctorId == id).Select(t => t.TreatmentId).ToList();

                    foreach (int id in treatmentIds)
                    {
                        treatmentDetails.AddRange(details.Where<TreatmentDetails>(t => t.TreatmentId == id).ToList());
                    }

                    return treatmentDetails;
                }));

            // get treatmentDetail by id
            _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .Returns(async (int id) => await Task.Run(() =>
                    details.Where<TreatmentDetails>(d => d.TreatmentDetailsId == id).FirstOrDefault()));

            // get a single most recent patient detail
            _mockRepo.Setup(repo => repo.GetSinglePatientTreatment(It.IsAny<int>()))
                .Returns(async (int id) => await Task.Run(() =>
                {
                    var treatmentDetails = details.Where<TreatmentDetails>(td => td.PatientId == id).ToList();
                    DateTime maxDate = DateTime.MinValue;
                    foreach (var e in treatmentDetails)
                    {
                        if (DateTime.Parse(e.StartTime) > maxDate)
                        {
                            maxDate = DateTime.Parse(e.StartTime);
                        }
                    }
                    var detail = details.FirstOrDefault(e => e.StartTime == maxDate.ToString());

                    return detail;
                }));

            // create treatmentDetail
            _mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<TreatmentDetails>()))
                .Returns(async (TreatmentDetails doctor) => await Task.Run(() => details.Add(doctor)));

            // updates treatmentDetail
            _mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<TreatmentDetails>()))
                .Returns(async (TreatmentDetails doctor) => await Task.Run(() => details));

            /*// deletes a treatmentDetail
            _mockRepo.Setup(repo => repo.DeleteAsync(It.IsAny<TreatmentDetails>()))
                .Returns(async (TreatmentDetails doctor) => await Task.Run(() => details.Remove(doctor)));*/
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
        public async void GetPatientsTreatment_ActionExecutes_ReturnsOKStatus()
        {
            var result = await _controller.GetPatientsTreatment(1);

            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async void GetTreatmentDetailsByDoctor_ActionExecutes_ReturnsOKStatus()
        {
            var result = await _controller.GetTreatmentDetailsByDoctor(1);

            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async void GetTreatmentDetail_Action_ReturnsOK()
        {
            var result = await _controller.GetTreatmentDetail(1);

            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        /*[Fact]
        public async void GetTreatmentDetail_Action_ReturnsNotFound()
        {
            var result = await _controller.GetTreatmentDetail(23);

            var notFoundResult = result as NotFoundResult;

            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
        }*/

        /*[Fact]
        public async void GetSinglePatientsTreatment_Action_ReturnsOK()
        {
            var result = await _controller.GetSinglePatientsTreatment(1);

            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }*/

        /*[Fact]
        public async void GetSinglePatientsTreatment_Action_ReturnsNotFound()
        {
            var result = await _controller.GetSinglePatientsTreatment(23);

            var notFoundResult = result as NotFoundResult;

            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
        }*/

        [Fact]
        public async void Post_Action_ReturnsCreatedAtAction()
        {
            TreatmentDetails detail = new TreatmentDetails(3, 1, 1, 1, "Dummy");
            var result = await _controller.Post(detail);

            var createdResult = result as CreatedAtActionResult;

            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);
        }

        /*[Fact]
        public async void Post_Action_ReturnsConflict_OnDuplicate()
        {
            TreatmentDetails detail = new TreatmentDetails(1, 1, 1, 1, "Dummy");
            var result = await _controller.Post(detail);

            var createdResult = result as ConflictResult;

            Assert.NotNull(createdResult);
            Assert.Equal(409, createdResult.StatusCode);
        }*/

        /*[Fact]
        public async void Put_Action_ReturnsNoContent()
        {
            TreatmentDetails detail = new TreatmentDetails(1, 1, 2, 1, "Dummy");
            var result = await _controller.Put(1, detail);

            var noContentResult = result as NoContentResult;

            Assert.NotNull(noContentResult);
            Assert.Equal(204, noContentResult.StatusCode);
        }

        [Fact]
        public async void Put_Action_ReturnsBadRequest()
        {
            TreatmentDetails detail = new TreatmentDetails(1, 1, 2, 1, "Dummy");
            var result = await _controller.Put(2, detail);

            var badRequestResult = result as BadRequestResult;

            Assert.NotNull(badRequestResult);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async void Put_Action_ReturnsNotFound()
        {
            TreatmentDetails detail = new TreatmentDetails(23, 1, 2, 1, "Dummy");
            var result = await _controller.Put(23, detail);

            var notFoundResult = result as NotFoundResult;

            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
        }*/

        /*[Fact]
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
        }*/
    }
}
