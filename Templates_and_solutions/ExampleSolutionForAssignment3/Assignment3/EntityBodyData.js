
// Entity body data

// Employee

var lauraEmployee =
    {
        "ReportsTo": 6,
        "Employee2":
            {
                "EmployeeId": 6,
                "LastName": "Mitchell",
                "FirstName": "Michael",
                "Title": "IT Manager",
                "BirthDate": "1973-07-01T00:00:00",
                "HireDate": "2003-10-17T00:00:00",
                "Address": "5827 Bowness Road NW",
                "City": "Calgary",
                "State": "AB",
                "Country": "Canada",
                "PostalCode": "T3B 0C5",
                "Phone": "+1 (403) 246-9887",
                "Fax": "+1 (403) 246-9899",
                "Email": "michael@chinookcorp.com"
            },
        "Employee1": [],
        "EmployeeId": 8,
        "LastName": "Callahan",
        "FirstName": "Laura",
        "Title": "IT Staff",
        "BirthDate": "1968-01-09T00:00:00",
        "HireDate": "2004-03-04T00:00:00",
        "Address": "923 7 ST NW",
        "City": "Lethbridge",
        "State": "AB",
        "Country": "Canada",
        "PostalCode": "T1H 1Y8",
        "Phone": "+1 (403) 467-3351",
        "Fax": "+1 (403) 467-8772",
        "Email": "laura@chinookcorp.com"
    }

var newEmployee =
    {
        "ReportsTo": 6,
        "LastName": "McIntyre",
        "FirstName": "Peter",
        "Title": "Professor",
        "BirthDate": "1978-01-09T00:00:00",
        "HireDate": "2014-03-04T00:00:00",
        "Address": "70 The Pond Road",
        "City": "Toronto",
        "State": "ON",
        "Country": "Canada",
        "PostalCode": "M3J 3M6",
        "Phone": "+1 (416) 491-5050",
        "Fax": "+1 (416) 491-5055",
        "Email": "peter@example.com"
    }

// Michael employee identifier is 6
// Nancy employee identifier is 2

var setPeterSupervisorToMichael = 
    {
        "EmployeeId": 9,
        "ReportsToId": 6
    }

var setPeterSupervisorToNancy =
    {
        "EmployeeId": 9,
        "ReportsToId": 2
    }

var michaelDirectReportsOriginal = 
    {
        "EmployeeIds": [7, 8, 9],
        "EmployeeId": 6
    }

var michaelDirectReportsNew = 
    {
        "EmployeeIds": [7, 8],
        "EmployeeId": 6
    }
