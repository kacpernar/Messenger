async function setImageUsingStreaming(imageElementId, imageStream) {
    var base64String;
    const arrayBuffer = await imageStream.arrayBuffer();
    const blob = new Blob([arrayBuffer]);

    var reader = new FileReader();
    reader.readAsDataURL(blob);
    reader.onloadend = function () {
        base64String = reader.result;
        document.getElementById(imageElementId).src = base64String;
    }
    return await base64String;
}