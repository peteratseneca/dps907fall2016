// JavaScript source code for HTML5 app

var baseUrl = 'http://localhost:1075';

function fetchArtists() {

    // Get a reference to the DOM element
    var s = document.querySelector('#status');

    // create an xhr object
    var xhr = new XMLHttpRequest();

    // configure its handler
    xhr.onreadystatechange = function () {

        if (xhr.readyState === 4) {
            // request-response cycle has been completed, so continue

            if (xhr.status === 200) {
                // request was successfully completed, and data was received, so continue

                // update the user interface
                s.innerHTML = '';

                // If you're interested in seeing the returned JSON...
                // Open the browser developer tools, and look in the JavaScript console
                console.log(xhr.responseText);

                // Call the UI-building method
                writeArtistList(JSON.parse(xhr.responseText));

            } else {
                // Get a reference to the DOM element
                var e = document.querySelector('#artistList');
                e.innerHTML = "<p>Request was not successful<br>(" + xhr.statusText + ")</p>";
                s.innerHTML = '';
            }
        } else {
            s.innerHTML = 'Loading...';
        }
    }

    // configure the url
    var artistsUrl = baseUrl + '/api/artists';
    // configure the xhr object to fetch content
    xhr.open('get', artistsUrl, true);
    // set the request header
    xhr.setRequestHeader('accept', 'application/json');
    // fetch the content
    xhr.send(null);
}

function writeArtistList(results) {

    // Get the parts of the response
    var links = results.Links;
    var collection = results.Collection;
    var collLinkHref = results.Links[0].Href;

    // Begin table
    var table = '<table class="table"><tr><th></th><th>Artist name</th></tr>';

    for (var i = 0; i < collection.length; i++) {

        table += '<tr><td>' +
            '<a href=artistdetail.html?artistid=' + collection[i].ArtistId + ' alt="">Details</a>' +
            '</td><td>' +
            collection[i].Name +
            '</td></tr>';
    }

    // End table
    table += '</table>';

    // Get a reference to the DOM element
    var e = document.querySelector('#artistList');
    e.innerHTML = table;
}

// Get query string components
var getQueryString = function (field, url) {
    var href = url ? url : window.location.href;
    var reg = new RegExp('[?&]' + field + '=([^&#]*)', 'i');
    var string = reg.exec(href);
    return string ? string[1] : null;
};

function fetchArtistDetail() {

    // Get a reference to the DOM element
    var s = document.querySelector('#status');

    // create an xhr object
    var xhr = new XMLHttpRequest();

    // configure its handler
    xhr.onreadystatechange = function () {

        if (xhr.readyState === 4) {
            // request-response cycle has been completed, so continue

            if (xhr.status === 200) {
                // request was successfully completed, and data was received, so continue

                // update the user interface
                s.innerHTML = '';

                // If you're interested in seeing the returned JSON...
                // Open the browser developer tools, and look in the JavaScript console
                console.log(xhr.responseText);

                // Call the UI-building method
                writeArtistDetail(JSON.parse(xhr.responseText));

            } else {
                // Get a reference to the DOM element
                var e = document.querySelector('#artistList');
                e.innerHTML = "<p>Request was not successful<br>(" + xhr.statusText + ")</p>";
                s.innerHTML = '';
            }
        } else {
            s.innerHTML = 'Loading...';
        }
    }

    // fetch the artist identifier from the query string
    var artistid = getQueryString('artistid');
    // configure the url
    var artistUrl = baseUrl + '/api/artists/' + artistid;
    // configure the xhr object to fetch content
    xhr.open('get', artistUrl, true);
    // set the request header
    xhr.setRequestHeader('accept', 'application/json');
    // fetch the content
    xhr.send(null);
}

function writeArtistDetail(results) {

    // Get the parts of the response
    var links = results.Links;
    var collLinkHref = results.Links[0].Href;
    var item = results.Item;
    var albums = item.Albums;

    // Begin table
    var table = '<table class="table"><tr><th></th><th>Album title</th></tr>';

    for (var i = 0; i < albums.length; i++) {

        table += '<tr><td>' +
            '<a href=albumdetail.html?albumid=' + albums[i].AlbumId + ' alt="">Details</a>' +
            '</td><td>' +
            albums[i].Title +
            '</td></tr>';
    }

    // End table
    table += '</table>';

    // Get a reference to the DOM elements, and update
    var e = document.querySelector('#artistDetail');
    e.innerHTML = table;
    e = document.querySelector('#artistName');
    e.innerHTML = 'Artist details - ' + item.Name;
}
