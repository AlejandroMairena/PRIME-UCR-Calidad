
//esta funcion recibe el path y el nombre del archivo (tiene que estar desencriptado)
function showImage(path, filename) {
    let modal = document.getElementById("myModal");
    let modalImg = document.getElementById("img01");
    let captionText = document.getElementById("caption");

    modal.style.display = "block";
    modalImg.src = path;
    captionText.innerHTML = filename;
    return true;
}
function closeView() {
    let modal = document.getElementById("myModal");
    modal.style.display = "none";
    return true;
}

