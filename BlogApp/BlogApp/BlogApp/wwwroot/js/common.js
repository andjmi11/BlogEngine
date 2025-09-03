window.bootstrapInterop = {
    showModal: function (id) {
        var el = document.getElementById(id);
        var modal = bootstrap.Modal.getInstance(el);
        if (!modal) {
            modal = new bootstrap.Modal(el);
        }
        modal.show();
    },
    hideModal: function (id) {
        var el = document.getElementById(id);
        var modal = bootstrap.Modal.getInstance(el);
        if (!modal) {
            modal = new bootstrap.Modal(el);
        }
        modal.hide();
    }
};

window.closeOffcanvas = (id) => {
    var offcanvasEl = document.getElementById(id);
    var bsOffcanvas = bootstrap.Offcanvas.getInstance(offcanvasEl);
    if (!bsOffcanvas) {
        bsOffcanvas = new bootstrap.Offcanvas(offcanvasEl);
    }
    bsOffcanvas.hide();
}
