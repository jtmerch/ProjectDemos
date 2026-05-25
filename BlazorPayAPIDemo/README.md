# PaymentGatewayDemo

This is a .NET demo project that shows a payment gateway-style architecture using ASP.NET Core Web APIs, Blazor Server, async API calls, dependency injection, inheritance, merchant lookup, and xUnit tests.

## Projects

- `PaymentGatewayApi` - main/front-end Web API. Receives payment authorization requests.
- `FraudApi` - backend API that simulates a fraud/risk check with `Task.Delay(3000)`.
- `ProcessorApi` - backend API that simulates a payment processor with `Task.Delay(5000)`.
- `PaymentBlazorClient` - optional Blazor Server UI that calls only the gateway API.
- `PaymentGatewayApi.Tests` - xUnit tests for services and orchestration logic.

## Flow

```text
Blazor / Swagger / Postman
        |
        v
PaymentGatewayApi
        |
        |-- Merchant lookup
        |-- FraudApi async call
        |-- ProcessorApi async call
        |
        v
Combined authorization response
```

The gateway starts the fraud call and processor call at the same time and waits with `Task.WhenAll`.

## API URLs to run locally

Set the launch URLs in Visual Studio to match these or update `appsettings.json`.

- PaymentGatewayApi: `https://localhost:7201`
- FraudApi: `https://localhost:7202`
- ProcessorApi: `https://localhost:7203`
- PaymentBlazorClient: `https://localhost:7140`

## Sample POST request

POST to:

```http
https://localhost:7201/api/payments/authorize
```

Body:

```json
{
  "merchantId": "M1001",
  "amount": 125.50,
  "currency": "USD",
  "cardNumber": "4111111111111111",
  "expirationMonth": 12,
  "expirationYear": 2030,
  "cvv": "123",
  "customerName": "Test Customer",
  "idempotencyKey": "unique-key-abc123"
}
```

> **idempotencyKey** (optional): A unique string you choose for each payment attempt. If you send the same request twice with the same key (for example, because of a network retry), the gateway returns the original response instead of charging the customer again. The cached result expires after 24 hours.

Expected approved response:

```json
{
  "transactionId": "generated-guid",
  "approved": true,
  "responseMessage": "Payment approved.",
  "authCode": "123456",
  "fraudScore": 15,
  "processorResponseCode": "00",
  "merchantName": "Savannah Urgent Care",
  "processorName": "SimulatedProcessor",
  "cardNetwork": "Visa",
  "velocityFlag": false,
  "fraudChecks": ["Amount OK", "Card OK"]
}
```

> **cardNetwork**: The card brand detected from the card number (e.g., Visa, Mastercard).
> **velocityFlag**: `true` if the fraud check flagged this card for too many recent transactions.
> **fraudChecks**: A list of fraud check labels that were run and passed.

## Other sample requests

### Merchant not found

```json
{
  "merchantId": "BADID",
  "amount": 125.50,
  "currency": "USD",
  "cardNumber": "4111111111111111",
  "expirationMonth": 12,
  "expirationYear": 2030,
  "cvv": "123",
  "customerName": "Test Customer"
}
```

### Inactive merchant

```json
{
  "merchantId": "M9999",
  "amount": 125.50,
  "currency": "USD",
  "cardNumber": "4111111111111111",
  "expirationMonth": 12,
  "expirationYear": 2030,
  "cvv": "123",
  "customerName": "Test Customer"
}
```

### Fraud decline

```json
{
  "merchantId": "M1001",
  "amount": 6000.00,
  "currency": "USD",
  "cardNumber": "4111111111111111",
  "expirationMonth": 12,
  "expirationYear": 2030,
  "cvv": "123",
  "customerName": "Test Customer"
}
```

### Processor decline

```json
{
  "merchantId": "M1001",
  "amount": 125.50,
  "currency": "USD",
  "cardNumber": "4111111111110000",
  "expirationMonth": 12,
  "expirationYear": 2030,
  "cvv": "123",
  "customerName": "Test Customer"
}
```

### GET status

```http
GET https://localhost:7201/api/payments/status/abc123
```

## Interview talking points

- The gateway API is the public/front-end API.
- The Blazor app is only a client and does not call backend services directly.
- Merchant lookup happens before external calls.
- Fraud and processor calls run asynchronously with `Task.WhenAll`.
- Backend APIs use `Task.Delay` only to simulate real external latency.
- Controllers stay thin; business logic is in services.
- Dependencies are injected through interfaces.
- `IHttpClientFactory` is used instead of manually creating `HttpClient`.
- xUnit tests cover merchant lookup and payment orchestration.
- This can be secured with HTTPS, JWT bearer auth, OAuth/OIDC, API keys between services, CORS, rate limiting, request validation, and Azure Key Vault for secrets.
- In-memory merchant storage is used for the demo, but the repository pattern allows replacing it with SQL Server, EF Core, or Dapper.

## OOP/inheritance included

- `PaymentRequestBase` -> `CardPaymentRequest`
- `PaymentProcessorBase` -> `SimulatedPaymentProcessor`
- Interfaces for services and clients

## Run order

Start these first:

1. FraudApi
2. ProcessorApi
3. PaymentGatewayApi
4. PaymentBlazorClient

Then submit from the Blazor page, Swagger, Postman, or curl.
