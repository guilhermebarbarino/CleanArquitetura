﻿using Domain.Entities;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tests
{
    public class ProductTest
    {
        [Fact]
        public void CreateProduct_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Product("Product Name", "Product Description", 9.99m,
                99, "product image");
            action.Should()
                .NotThrow<Validation.DomainExceptionValidation>();
        }


        [Fact]
        public void CreateProduct_ShortNameValue_DomainExceptionShortName()
        {
            Action action = () => new Product(1, "Pr", "Product Description", 9.99m,
                99, "product image");
            action.Should().
                Throw<Validation.DomainExceptionValidation>().
                WithMessage("Invalid name, too short, minimum 3 characters");
        }


        [Fact]
        public void CreateProduct_NegativeValue_DomainExceptionInvalid()
        {
            Action action = () => new Product(-1, "Product Name", "Product Description", 9.99m,
                99, "product image");
            action.Should().
                Throw<Validation.DomainExceptionValidation>().
                WithMessage("Invalid id value");
        }

        [Fact]
        public void CreateProduct_LongImageName_DomainExceptionLongImageName()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m,
                99, "product image tooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo loooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo");
            action.Should().
                Throw<Validation.DomainExceptionValidation>().
                WithMessage("Invalid image name, too long, maximum 250 characters");

        }

        [Fact]
        public void CreateProduct_WithNullImageName_NoDomainException()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m,
                99, null);
            action.Should().
                NotThrow<Validation.DomainExceptionValidation>();
        }

        [Fact]
        public void CreateProduct_WithEmptyImageName_NoDomainException()
        {
            Action action = () => new Product("Product Name", "Product Description", 9.99m,
                99, "");
            action.Should().
                NotThrow<Validation.DomainExceptionValidation>();
        }

        [Fact]
        public void CreateProduct_InvalidPriceValue_DomainException()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", -9.99m,
                99, "");
            action.Should().
            Throw<Validation.DomainExceptionValidation>().
                WithMessage("Invalid price value");
        }

        [Theory]
        [InlineData(-5)]
        public void CreateProduct_InvalidStockValue_ExceptionDomainNegativeValue(int value)
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m,
                value, "product image");
            action.Should().
            Throw<Validation.DomainExceptionValidation>().
                WithMessage("Invalid stock value");
        }

    }
}
