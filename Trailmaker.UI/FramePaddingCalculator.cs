using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Trailmaker.UI
{
    public class FramePaddingCalculator
    {
        public Thickness CalculatePadding(double offsetX, double offsetY, double sigmaX, double sigmaY)
        {
            var offsetCalculator = new OffsetCalculator();
            var offsetXPadding = offsetCalculator.Calculate(offsetX);
            var offsetYPadding = offsetCalculator.Calculate(offsetY);

            var paddingLeft = offsetXPadding.PaddingStart;
            var paddingRight = offsetXPadding.PaddingEnd;
            var paddingTop = offsetYPadding.PaddingStart;
            var paddingBottom = offsetYPadding.PaddingEnd;

            var sigmaCalculator = new SigmaCalculator();
            var sigmaXPadding = sigmaCalculator.Calculate(sigmaX, offsetX);
            var sigmaYPadding = sigmaCalculator.Calculate(sigmaY, offsetY);

            paddingLeft += sigmaXPadding.PaddingStart;
            paddingRight += sigmaXPadding.PaddingEnd;

            paddingTop += sigmaYPadding.PaddingStart;
            paddingBottom += sigmaYPadding.PaddingEnd;

            return new Thickness(paddingLeft, paddingTop, paddingRight, paddingBottom);
        }

        private class OffsetCalculator
        {
            public PaddingResult Calculate(double offset)
            {
                var result = new PaddingResult();

                if (offset > 0)
                {
                    result.PaddingEnd = offset;
                }
                else
                {
                    result.PaddingStart = offset * -1;
                }

                return result;
            }
        }

        private class SigmaCalculator
        {
            internal PaddingResult Calculate(double sigma, double offset)
            {
                var result = new PaddingResult();

                result.PaddingStart = sigma * 2;

                if (offset > 0)
                {
                    result.PaddingStart -= offset;

                    if (result.PaddingStart < 0)
                    {
                        result.PaddingStart = 0;
                    }
                }

                result.PaddingEnd = sigma * 2;

                return result;
            }
        }

        private class PaddingResult
        {
            public double PaddingStart { get; set; }
            public double PaddingEnd { get; set; }
        }
    }
}
