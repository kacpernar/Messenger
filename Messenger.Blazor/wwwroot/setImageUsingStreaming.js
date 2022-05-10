async function setImageUsingStreaming(imageElementId, imageStream) {
    var base64String;

    async function cb(rb) {
        base64String = rb;
        return base64String;
    }

    async function f(callback) {
        const arrayBuffer = await imageStream.arrayBuffer();
        const blob = new Blob([arrayBuffer]);

        var reader = new FileReader();
        reader.readAsDataURL(blob);
        reader.onloadend = event => {
            document.getElementById(imageElementId).src = reader.result;
            callback(reader.result)
        }

    }
    
    return await f(cb);
}