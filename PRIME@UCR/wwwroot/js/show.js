
//esta funcion recibe el path y el nombre del archivo (tiene que estar desencriptado)
function showMultimedia(path, filename, type) {
    switch (type) {
        case "application/pdf":
            showPdf(path, filename);
            break;

        case "image/png":
            showImage(path, filename);
            break;
    }
    return true;
}
function showImage(path, filename) {
    console.log("ESTOY MOSTRANDO UNA IMAGEN");
    let modal = document.getElementById("myModal");
    let modalImg = document.getElementById("img01");
    let captionText = document.getElementById("caption");

    modal.style.display = "block";
    modalImg.style.display = "block";
    captionText.style.display = "block";
    modalImg.src = path;
    captionText.innerHTML = filename;
    return true;
}
function showPdf(path,filename) {
    console.log("ESTOY MOSTRANDO UN PDF");
    let win = window.open(path,'_blank');
    win.focus();
}


function closeView() {
    let modal = document.getElementById("myModal");
    modal.style.display = "none";
    return true;
}

