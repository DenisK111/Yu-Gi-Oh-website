using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Yu_Gi_Oh_website.Data.Data.Seeding.Common;
using Yu_Gi_Oh_website.Services.Forum.Contracts;
using Yu_Gi_Oh_website.Web.Areas.Administration.Models;

namespace Yu_Gi_Oh_website.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = Roles.Admin)]
    public class SubCattegoryController : Controller
    {
        private readonly ISubCattegoryService subCattegoryService;
        private readonly IToastNotification toastNotification;
        private readonly ICattegoryService cattegoryService;
        private readonly IMapper mapper;

        public SubCattegoryController(ISubCattegoryService subCattegoryService, IToastNotification toastNotification, ICattegoryService cattegoryService, IMapper mapper)
        {
            this.subCattegoryService = subCattegoryService;
            this.toastNotification = toastNotification;
            this.cattegoryService = cattegoryService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> SubCattegories()
        {

            return this.View(await GenerateSubGattegoryViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> SubCattegories(SubCattegoryViewModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    toastNotification.AddErrorToastMessage(error.ErrorMessage);
                }

                return this.RedirectToAction(nameof(SubCattegories));
            }

            var result = await subCattegoryService.EditSubCattegoryAsync(inputModel.Id, inputModel.Name, inputModel.Description, inputModel.CattegoryId,inputModel.IsDeleted);

            if (result)
            {
                toastNotification.AddSuccessToastMessage($"SubCattegory {inputModel.Id} - {inputModel.Name} has been updated successfully!");
            }

            else
            {
                toastNotification.AddErrorToastMessage("Update failed.");
            }

            return this.RedirectToAction(nameof(SubCattegories));

        }

        [HttpGet] 
        public async Task<IActionResult> AddSubCattegory()
        {
            var viewModel = new AddSubCattegoryInputModel();
            viewModel.Cattegories = await cattegoryService.GetCattegoryIdNameAsync();
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddSubCattegory(AddSubCattegoryInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    toastNotification.AddErrorToastMessage(error.ErrorMessage);
                }
                return this.View(inputModel);
            }

            var success = await subCattegoryService.AddSubCattegoryAsync(inputModel.Name, inputModel.Description, inputModel.CattegoryId);

            if (success)
            {
                toastNotification.AddSuccessToastMessage("SubCattegory has been added successfully!");
            }

            else
            {
                toastNotification.AddErrorToastMessage("Failed to add SubCattegory. SubCattegory already exists.");
            }

            return this.RedirectToAction(nameof(SubCattegories));
        }

        private async Task<SubCattegoriesViewModel> GenerateSubGattegoryViewModel()
        {
            var subCattegories = await subCattegoryService.GetSubCattegoryAdminDetailsAsync();
            var subCattegoriesModel = mapper.Map<IEnumerable<SubCattegoryViewModel>>(subCattegories);
            var cattegories = (await cattegoryService.GetCattegoryIdNameAsync()).ToList();

            var modelToReturn = new SubCattegoriesViewModel()
            {
                SubCattegories = subCattegoriesModel,
                Cattegories = cattegories
            };

            return modelToReturn;
        }
    }
}
