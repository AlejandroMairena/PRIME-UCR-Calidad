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

function openCamera() {
    const constraints = {
        video: { width: { exact: 640 }, height: { exact: 480 } }
    };

    const video = document.querySelector('video');

    navigator.mediaDevices.getUserMedia(constraints).
        then((stream) => { video.srcObject = stream });

    return true;
}