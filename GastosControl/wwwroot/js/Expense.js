function addDetalle() {
    const container = document.getElementById('detalles-container');

    const html = `
        <div class="detalle-item mb-3 border p-3 rounded">
            <div class="form-group">
                <label>Tipo de Gasto</label>
                <select name="Details[${detalleIndex}].ExpenseTypeId" class="form-control">
                    ${selectTemplate}
                </select>
            </div>
            <div class="form-group">
                <label>Monto</label>
                <input type="number" name="Details[${detalleIndex}].Amount" class="form-control" />
            </div>
            <button type="button" class="btn btn-danger btn-sm mt-2" onclick="removeDetalle(this)">Eliminar</button>
        </div>`;

    container.insertAdjacentHTML('beforeend', html);
    detalleIndex++;
}


function removeDetalle(button) {
    button.closest('.detalle-item').remove();
}
