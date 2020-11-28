

function showTxt(path) {
    console.log("ESTOY MOSTRANDO UN PDF");
    let win = window.open(path,'_blank');
    win.focus();

    //sleep(3000);
    return true;
}
