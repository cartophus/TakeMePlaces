var map;
var userPos;
var infoWindow;
var request;
var service;
var markers = [];
var category1;

function display(category) {

    var bucharest = new google.maps.LatLng(44.4268, 26.1025);
    category1 = category;

    map = new google.maps.Map(document.getElementById('map'), {
        center: bucharest,
        zoom: 14,
        styles: [
            { elementType: 'geometry', stylers: [{ color: '#242f3e' }] },
            { elementType: 'labels.text.stroke', stylers: [{ color: '#242f3e' }] },
            { elementType: 'labels.text.fill', stylers: [{ color: '#746855' }] },
            {
                featureType: 'administrative.locality',
                elementType: 'labels.text.fill',
                stylers: [{ color: '#d59563' }]
            },
            {
                featureType: 'poi',
                elementType: 'labels.text.fill',
                stylers: [{ color: '#d59563' }]
            },
            {
                featureType: 'poi.park',
                elementType: 'geometry',
                stylers: [{ color: '#263c3f' }]
            },
            {
                featureType: 'poi.park',
                elementType: 'labels.text.fill',
                stylers: [{ color: '#6b9a76' }]
            },
            {
                featureType: 'road',
                elementType: 'geometry',
                stylers: [{ color: '#38414e' }]
            },
            {
                featureType: 'road',
                elementType: 'geometry.stroke',
                stylers: [{ color: '#212a37' }]
            },
            {
                featureType: 'road',
                elementType: 'labels.text.fill',
                stylers: [{ color: '#9ca5b3' }]
            },
            {
                featureType: 'road.highway',
                elementType: 'geometry',
                stylers: [{ color: '#746855' }]
            },
            {
                featureType: 'road.highway',
                elementType: 'geometry.stroke',
                stylers: [{ color: '#1f2835' }]
            },
            {
                featureType: 'road.highway',
                elementType: 'labels.text.fill',
                stylers: [{ color: '#f3d19c' }]
            },
            {
                featureType: 'transit',
                elementType: 'geometry',
                stylers: [{ color: '#2f3948' }]
            },
            {
                featureType: 'transit.station',
                elementType: 'labels.text.fill',
                stylers: [{ color: '#d59563' }]
            },
            {
                featureType: 'water',
                elementType: 'geometry',
                stylers: [{ color: '#17263c' }]
            },
            {
                featureType: 'water',
                elementType: 'labels.text.fill',
                stylers: [{ color: '#515c6d' }]
            },
            {
                featureType: 'water',
                elementType: 'labels.text.stroke',
                stylers: [{ color: '#17263c' }]
            }
        ]
    });

    //center to user's location - else remains on default center
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            userPos = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);

            var marker = new google.maps.Marker({
                position: userPos,
                icon: {
                    path: 'M 125,5 155,90 245,90 175,145 200,230 125,180 50,230 75,145 5,90 95,90 z',
                    fillColor: 'yellow',
                    fillOpacity: 0.8,
                    scale: 0.1,
                    strokeColor: 'gold',
                    strokeWeight: 1
                },
                map: map
            });

            infoWindow = new google.maps.InfoWindow();

            google.maps.event.addListener(marker, 'click', function () {
                infoWindow.setContent('You are here');
                infoWindow.open(map, this);
            });


            request = {
                location: userPos,
                radius: '2000',
                type: [category] 
            }

            infoWindow = new google.maps.InfoWindow();

            service = new google.maps.places.PlacesService(map);

            service.nearbySearch(request, callback);

            map.setCenter(userPos);
        });

    } else {

        request = {
            location: bucharest,
            radius: '2000',
            type: [category]
        }

        infoWindow = new google.maps.InfoWindow();

        service = new google.maps.places.PlacesService(map);

        service.nearbySearch(request, callback);
    }

    google.maps.event.addListener(map, 'rightclick', function (event) {

        map.setCenter(event.latLng);

        clearResults(markers);

        service = new google.maps.places.PlacesService(map);

        request = {
            location: event.latLng,
            radius: '2000',
            type: [category]
        }

        service.nearbySearch(request, callback);
    })
}

///STORE PLACE ID FOR AI ----------------------------------- TO DO
function callback(results, status) {
    //if (status == google.maps.places.PlacesService.OK) {
    for (var i = 0; i < results.length; i++) {
        markers.push(createMarker(results[i]));
    }
    //}
}

function createMarker(place) {
    
    var marker = new google.maps.Marker({
        position: place.geometry.location,
        map: map,
        animation: google.maps.Animation.DROP
    });

    google.maps.event.addListener(marker, 'click', function () {
        var photos = place.photos;
        var url;
        if (photos) url = photos[0].getUrl({ minWidth: 200, minHeight: 200, maxWidth:250, maxHeight: 250 });
        infoWindow.setContent(
            '<div>' + 
            '<img style="display: block;margin - left: auto;margin - right: auto;" src="' + url + '"></img>' +
            '<br /><h3><strong>' + place.name + '</strong></h3><br />' +
            '<p>Google Rating: ' + place.rating + '</p><br />' + 
            '<br /><p> Your Rating:</p>' +
            '<form method="post" action="/Home/Rate/' + category1 + '/' + place.place_id + '">' + 
            '<fieldset>' + 
            '<span class="star-cb-group">' + 
                '<input type="submit" id="rating-5" name="rating" value="5" /><label for="rating-5">5</label>' + 
                '<input type="submit" id="rating-4" name="rating" value="4" checked="checked" /><label for="rating-4">4</label>' + 
                '<input type="submit" id="rating-3" name="rating" value="3" /><label for="rating-3">3</label>' + 
                '<input type="submit" id="rating-2" name="rating" value="2" /><label for="rating-2">2</label>' + 
                '<input type="submit" id="rating-1" name="rating" value="1" /><label for="rating-1">1</label>' + 
                '<input type="submit" id="rating-0" name="rating" value="0" class="star-cb-clear" /><label for="rating-0">0</label>' + 
            '</span>' + 
            '</fieldset >' +
            '</form >' + 
            '</div>'
        );
        infoWindow.open(map, this);
    });
    return marker;
}

function clearResults(markers) {
    for (var m in markers) {
        markers[m].setMap(null);
    }
    markers = [];
}