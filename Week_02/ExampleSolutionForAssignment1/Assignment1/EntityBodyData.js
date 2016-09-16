
/*
    Here are the SmartphoneAdd properties...

    [Required, StringLength(100)]
    public string Manufacturer { get; set; }

    [Required, StringLength(100)]
    public string Model { get; set; }

    public DateTime DateReleased { get; set; }

    // Attention 09 - Data annotations for the resource model classes

    [Range(3.0, 10.0)]
    public double ScreenSize { get; set; }

    [Range(10, 10000)]
    public int MSRP { get; set; }
*/

var i6s =
    {
        "Manufacturer": "Apple",
        "Model": "iPhone 6s 16GB",
        "DateRelased": "2015-09-24T12:00:00",
        "ScreenSize": 4.7,
        "MSRP": 699
    };

var i6sp =
    {
        "Manufacturer": "Apple",
        "Model": "iPhone 6s Plus 16GB",
        "DateRelased": "2015-09-24T12:00:00",
        "ScreenSize": 5.5,
        "MSRP": 799
    };

var sgs6 =
    {
        "Manufacturer": "Samsung",
        "Model": "Galaxy S6 16GB",
        "DateRelased": "2015-08-24T12:00:00",
        "ScreenSize": 5.1,
        "MSRP": 699
    };

var badData =
    {
        "Manufacturer": "Samsung - Video provides a powerful way to help you prove your point. When you click Online Video, you can paste in the embed code for the video you want to add. You can also type a keyword to search online for the video that best fits your document. To make your document look professionally produced, Word provides header, footer, cover page, and text box designs that complement each other. For example, you can add a matching cover page, header, and sidebar. Click Insert and then choose the elements you want from the different galleries.",
        "Model": "Galaxy S6 16GB",
        "DateRelased": "2015-08-24T12:00:00",
        "ScreenSize": 25.1,
        "MSRP": 69900
    };
