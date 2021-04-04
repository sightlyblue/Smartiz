using NUnit.Framework;
using Quadratic_Equation_Plot;
using System;
using System.Collections.Generic;

namespace UnitTests
{
    public class PolinomTestFixture
    {
        [Test]
        public void Roots_LinearPolinom_ReturnsSingleRoot()
        {
            // Arrange
            var polinom = new List<double> { 0, 1 };

            // Act
            var roots = Polinom.Roots(polinom);

            // Assert
            Assert.AreEqual(1, roots.Length, "Should have exactly one root");
        }

        [Test]
        public void Extremes_LinearPolinom_ReturnsEmpty()
        {
            // Arrange
            var polinom = new List<double> { 0, 1 };

            // Act
            var extremes = Polinom.Extremes(polinom);

            // Assert
            Assert.AreEqual(0, extremes.Length, "Should have no extremes");
        }

    }
}
