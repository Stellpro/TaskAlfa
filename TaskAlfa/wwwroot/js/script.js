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
async function downloadFileFromStream(fileName, contentStreamReference) {
    const arrayBuffer = await contentStreamReference.arrayBuffer();
    const blob = new Blob([arrayBuffer]);

    const url = URL.createObjectURL(blob);

    triggerFileDownload(fileName, url);

    URL.revokeObjectURL(url);
}

function triggerFileDownload(fileName, url) {
    const anchorElement = document.createElement('a');
    anchorElement.href = url;

    if (fileName) {
        anchorElement.download = fileName;
    }

    anchorElement.click();
    anchorElement.remove();
}