function confirmDelete(url) {
    let rlt = confirm("Do you approve it will be deleted?");
    if (rlt) {
        window.location = url;

    }
}