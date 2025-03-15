# Background Worker for Fetching Flights from Amadeus API

## Overview
This project is a background worker built with **.NET 9** that fetches flight data from the **Amadeus API** and stores it in a **SQL Server** database. The worker can be configured to retrieve flight information based on user-defined parameters.

## Features
- Fetches flight data from **Amadeus API**.
- Stores retrieved flight details in a **SQL Server** database.
- Configurable parameters such as airports, date range, concurrency limits, and request delays.
- Supports **multiple concurrent API calls**.

## Prerequisites
- **.NET 9 SDK** installed.
- **SQL Server** instance available.
- **Amadeus API account** (Register at [Amadeus Developers](https://developers.amadeus.com/)).
- **Amadeus API Key & Secret** for authentication.

## Configuration
Modify the `appsettings.json` file to define the parameters for fetching flight data:

```json
{
  "TestData": {
    "Airports": [
      "DXB", // Dubai International Airport
      "IST"  // Istanbul Airport
    ],
    "FromDate": "29-04-2025",
    "ToDate": "29-04-2025",
    "MaxConcurrentCalls": 2,
    "MaxFlights": 2,
    "DelayBetweenCalls": 1
  }
}
```

### Configuration Parameters:
- **Airports**: List of airport codes to fetch flight data from.
- **FromDate / ToDate**: Date range for fetching flights.
- **MaxConcurrentCalls**: Maximum number of concurrent API requests.
- **MaxFlights**: Maximum number of flights to retrieve per request.
- **DelayBetweenCalls**: Delay (in milliseconds) between API calls.

## Installation & Running the Worker

### Clone the repository
```sh
git clone https://github.com/yourusername/your-repo.git
cd your-repo
```

### Set up Amadeus API credentials
- Add your API key and secret to `appsettings.json`.

### Build and run the worker
```sh
dotnet build
dotnet run
```

## Database Setup
Ensure your SQL Server is running and update the connection string in `appsettings.json`.
