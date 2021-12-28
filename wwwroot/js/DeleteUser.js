function confirmDelete(uId, isDeleteClicked) {
    const deleteSpan = `deleteSpan_${uId}`;
    const confirmDeleteSpan = `confirmDeleteSpan_${uId}`;

    if (isDeleteClicked) {
        window.$(`#${deleteSpan}`).hide();
        window.$(`#${confirmDeleteSpan}`).show();
    } else {
        window.$(`#${deleteSpan}`).show();
        window.$(`#${confirmDeleteSpan}`).hide();
    }
}