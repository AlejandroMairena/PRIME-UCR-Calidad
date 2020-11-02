function hasGetUserMedia() {
    var has_um = !!(navigator.mediaDevices &&
        navigator.mediaDevices.getUserMedia);

    if (has_um) {
        // Good to go!
        console.log('Good to go!');
    } else {
        //alert('getUserMedia() is not supported by your browser');
        console.log('Not good to go!');
    }
    return has_um;
}
