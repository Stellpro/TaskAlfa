function saveAsFile(filename, bytesBase64) {
    var link = document.createElement('a');
    link.download = filename;
    link.href = "data:application/octet-stream;base64," + bytesBase64;
    document.body.appendChild(link); // Needed for Firefox
    link.click();
    document.body.removeChild(link);
}

function GetBrowserInfo() {
    return navigator.userAgent;
}

async function start(callback) {
    document.addEventListener("paste", async e => {
        e.preventDefault();
        if (!e.clipboardData.files.length) {
            return;
        }
        const file = e.clipboardData.files[0];
        var FR = new FileReader();
  
        FR.onload = function (evt) {
            var x = new String;
            x = evt.target.result;
            console.log(typeof x);
            callback.invokeMethodAsync("SingleUpload", x);
        };
        FR.readAsDataURL(file);
      
    });
}
function FileSaveAs(filename, fileContent) {
    var link = document.createElement('a');
    
    link.download = filename;
    link.href = "data:image/png;base64," + encodeURIComponent(fileContent)
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}