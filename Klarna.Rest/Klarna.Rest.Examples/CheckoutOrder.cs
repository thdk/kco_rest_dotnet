﻿#region Copyright Header
//-----------------------------------------------------------------------
// <copyright file="CheckoutOrder.cs" company="Klarna AB">
//     Copyright 2014 Klarna AB
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------
#endregion
namespace Klarna.Rest.Examples
{
    using System;
    using System.Collections.Generic;
    using Klarna.Rest.Checkout;
    using Klarna.Rest.Models;
    using Klarna.Rest.Transport;

    /// <summary>
    /// Checkout order resource examples.
    /// </summary>
    public class CheckoutOrder
    {
        #region Create

        /// <summary>
        /// Create a checkout order.
        /// </summary>
        public class CreateExample
        {
            /// <summary>
            /// Run the example code.
            /// </summary>
            public static void Main()
            {
                const string MerchantId = "0";
                const string SharedSecret = "sharedSecret";

                IConnector connector = ConnectorFactory.Create(MerchantId, SharedSecret, Client.TestBaseUrl);

                Client client = new Client(connector);
                ICheckoutOrder checkout = client.NewCheckoutOrder();

                OrderLine orderLine = new OrderLine
                {
                    Type = "physical",
                    Reference = "123050",
                    Name = "Tomatoes",
                    Quantity = 10,
                    QuantityUnit = "kg",
                    UnitPrice = 600,
                    TaxRate = 2500,
                    TotalAmount = 6000,
                    TotalTaxAmount = 1200
                };

                OrderLine orderLine2 = new OrderLine
                {
                    Type = "physical",
                    Reference = "543670",
                    Name = "Bananas",
                    Quantity = 1,
                    QuantityUnit = "bag",
                    UnitPrice = 5000,
                    TaxRate = 2500,
                    TotalAmount = 4000,
                    TotalDiscountAmount = 1000,
                    TotalTaxAmount = 800
                };

                MerchantUrls merchantUrls = new MerchantUrls
                {
                    Terms = new System.Uri("http://www.merchant.com/toc"),
                    Checkout = new System.Uri("http://www.merchant.com/checkout?klarna_order_url={checkout.order.url}"),
                    Confirmation = new System.Uri("http://www.merchant.com/thank-you?klarna_order_url={checkout.order.url}"),
                    Push = new System.Uri("http://www.merchant.com/create_order?klarna_order_id={checkout.order.id}")
                };

                CheckoutOrderData orderData = new CheckoutOrderData()
                {
                    PurchaseCountry = "gb",
                    PurchaseCurrency = "gbp",
                    Locale = "en-gb",
                    OrderAmount = 10000,
                    OrderTaxAmount = 2000,
                    OrderLines = new List<OrderLine> { orderLine, orderLine2 },
                    MerchantUrls = merchantUrls
                };

                checkout.Create(orderData);

                Uri checkoutUrl = checkout.Location;
            }
        }

        #endregion

        #region Fetch

        /// <summary>
        /// Retrieve a checkout order.
        /// </summary>
        public class FetchExample
        {
            /// <summary>
            /// Run the example code.
            /// </summary>
            public static void Main()
            {
                const string MerchantId = "0";
                const string SharedSecret = "sharedSecret";

                Uri orderUrl = new Uri("https://playground.api.klarna.com/checkout/v3/orders/12345");
                IConnector connector = ConnectorFactory.Create(MerchantId, SharedSecret, Client.TestBaseUrl);

                Client client = new Client(connector);
                ICheckoutOrder checkoutOrder = client.NewCheckoutOrder(orderUrl);

                CheckoutOrderData checkoutOrderData = checkoutOrder.Fetch();
            }
        }

        #endregion

        #region Update

        /// <summary>
        /// Update a checkout order.
        /// </summary>
        public class UpdateExample
        {
            /// <summary>
            /// Run the example code.
            /// </summary>
            public static void Main()
            {
                const string MerchantId = "0";
                const string SharedSecret = "sharedSecret";
                Uri orderUrl = new Uri("https://playground.api.klarna.com/checkout/v3/orders/12345");

                IConnector connector = ConnectorFactory.Create(MerchantId, SharedSecret, Client.TestBaseUrl);

                Client client = new Client(connector);
                ICheckoutOrder checkout = client.NewCheckoutOrder(orderUrl);

                CheckoutOrderData orderData = new CheckoutOrderData();
                orderData.OrderAmount = 11000;
                orderData.OrderTaxAmount = 2200;

                List<OrderLine> lines = new List<OrderLine>();

                lines.Add(new OrderLine()
                {
                    Type = "physical",
                    Reference = "123050",
                    Name = "Tomatoes",
                    Quantity = 10,
                    QuantityUnit = "kg",
                    UnitPrice = 600,
                    TaxRate = 2500,
                    TotalAmount = 6000,
                    TotalTaxAmount = 1200
                });

                lines.Add(new OrderLine()
                {
                    Type = "physical",
                    Reference = "543670",
                    Name = "Bananas",
                    Quantity = 1,
                    QuantityUnit = "bag",
                    UnitPrice = 5000,
                    TaxRate = 2500,
                    TotalAmount = 4000,
                    TotalDiscountAmount = 1000,
                    TotalTaxAmount = 800
                });

                lines.Add(new OrderLine()
                {
                    Type = "shipping_fee",
                    Name = "Express delivery",
                    Quantity = 1,
                    UnitPrice = 1000,
                    TaxRate = 2500,
                    TotalAmount = 1000,
                    TotalTaxAmount = 200
                });

                orderData.OrderLines = lines;

                orderData = checkout.Update(orderData);
            }
        }

        #endregion
    }
}
