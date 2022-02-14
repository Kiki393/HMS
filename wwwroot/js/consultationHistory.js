function viewHistory() {
    window.tinymce.init({
        selector: '#symptoms'
    });
    window.tinymce.init({
        selector: '#diagnosis'
    });

    window.$(".select").click(function () {
        const currentTds = window.$(this).closest("tr").find("td"); // find all td of selected row
        const cell0 = window.$(currentTds).eq(0).text(); // eq= cell , text = inner text
        const cell1 = window.$(currentTds).eq(1).text(); // eq= cell , text = inner text
        const cell2 = window.$(currentTds).eq(2).text();

        window.$("#symptoms").val(cell0);
        window.$("#diagnosis").val(cell1);
        window.$("#date").val(cell2);
        window.$("#consultationModal").modal("show");
    });
}

function closeHistory() {
    window.$("#consultationForm")[0].reset();
    window.$("#symptoms").val("");
    window.$("#diagnosis").val("");
    window.$("#date").val("");
    window.$("#consultationModal").modal("hide");
}