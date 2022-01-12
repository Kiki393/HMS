// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PredictionController.cs" company="VVU">
//   Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   Defines the AppointmentService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using AspNetCoreHero.ToastNotification.Abstractions;

    using Microsoft.Extensions.ML;

    /// <summary>
    /// The prediction controller.
    /// </summary>
    public class PredictionController : Controller
    {
        /// <summary>
        /// The notification service.
        /// </summary>
        private readonly INotyfService _notyf;

        /// <summary>
        /// Initializes a new instance of the <see cref="PredictionController"/> class.
        /// </summary>
        /// <param name="notyf">
        /// The notification service.
        /// </param>
        public PredictionController(INotyfService notyf)
        {
            this._notyf = notyf;
        }

        /// <summary>
        /// The predict.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult Predict()
        {
            var array = new byte[64];
            var image = new PneumoniaModel.ModelInput() { ImageSource = array };
            return this.View(image);
        }

        /// <summary>
        /// The predict.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PredictAsync(PneumoniaModel.ModelInput input)
        {
            if (!ModelState.IsValid)
            {
                return this.View(input);
            }

            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files.FirstOrDefault();
                await using var dataStream = new MemoryStream();
                if (file != null)
                {
                    await file.CopyToAsync(dataStream);
                }

                input.ImageSource = dataStream.ToArray();
            }

            try
            {
                var data = new PneumoniaModel.ModelInput()
                {
                    ImageSource = input.ImageSource,
                };

                // Load model and predict output
                var result = PneumoniaModel.Predict(data);
                var normAccuracy = result.Score[0] * 100;
                var pneumoniaAccuracy = result.Score[1] * 100;
                TempData["Label"] = result.PredictedLabel;
                TempData["Normal"] = normAccuracy.ToString(CultureInfo.CurrentCulture);
                TempData["Pneumonia"] = pneumoniaAccuracy.ToString(CultureInfo.CurrentCulture);
            }
            catch (Exception e)
            {
                this._notyf.Error(e.ToString());
            }

            return this.View();
        }
    }
}