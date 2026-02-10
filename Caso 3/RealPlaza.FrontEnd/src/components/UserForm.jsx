import React, { useState } from 'react';

export default function UserForm({ user, onSave, onClose }) {
  const isEditing = !!user;

  const formatDateForInput = (dateStr) => {
    if (!dateStr) return '';
    const d = new Date(dateStr);
    return d.toISOString().split('T')[0];
  };

  const [formData, setFormData] = useState({
    username: user?.username || '',
    passwordHash: '',
    email: user?.email || '',
    birthDate: formatDateForInput(user?.birthDate) || '',
    isActive: user?.isActive ?? true,
  });

  const [error, setError] = useState('');
  const [saving, setSaving] = useState(false);

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: type === 'checkbox' ? checked : value,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');

    if (!formData.username.trim()) {
      setError('El usuario es requerido.');
      return;
    }
    if (!isEditing && !formData.passwordHash.trim()) {
      setError('La contraseña es requerida.');
      return;
    }
    if (!formData.email.trim()) {
      setError('El email es requerido.');
      return;
    }
    if (!formData.birthDate) {
      setError('La fecha de nacimiento es requerida.');
      return;
    }

    setSaving(true);
    try {
      await onSave(formData);
    } catch (err) {
      setError(err.message || 'Error al guardar.');
    } finally {
      setSaving(false);
    }
  };

  return (
    <div className="modal-overlay" onClick={onClose}>
      <div className="modal-content" onClick={(e) => e.stopPropagation()}>
        <div className="modal-header">
          <h2>{isEditing ? 'Editar Usuario' : 'Nuevo Usuario'}</h2>
          <button className="btn-close" onClick={onClose}>×</button>
        </div>

        <form onSubmit={handleSubmit}>
          {/* Campo tipo Texto */}
          <div className="form-group">
            <label htmlFor="form-username">Usuario</label>
            <input
              id="form-username"
              type="text"
              name="username"
              placeholder="Nombre de usuario"
              value={formData.username}
              onChange={handleChange}
              required
            />
          </div>

          {/* Campo tipo Password (solo al crear) */}
          {!isEditing && (
            <div className="form-group">
              <label htmlFor="form-password">Contraseña</label>
              <input
                id="form-password"
                type="password"
                name="passwordHash"
                placeholder="Contraseña"
                value={formData.passwordHash}
                onChange={handleChange}
                required
              />
            </div>
          )}

          {/* Campo tipo Texto (email) */}
          <div className="form-group">
            <label htmlFor="form-email">Email</label>
            <input
              id="form-email"
              type="email"
              name="email"
              placeholder="correo@ejemplo.com"
              value={formData.email}
              onChange={handleChange}
              required
            />
          </div>

          {/* Campo tipo Selector de Fecha */}
          <div className="form-group">
            <label htmlFor="form-birthdate">Fecha de Nacimiento</label>
            <input
              id="form-birthdate"
              type="date"
              name="birthDate"
              value={formData.birthDate}
              onChange={handleChange}
              required
            />
          </div>

          {/* Campo tipo Checkbox */}
          {isEditing && (
            <div className="form-group form-group-checkbox">
              <label className="checkbox-label">
                <input
                  type="checkbox"
                  name="isActive"
                  checked={formData.isActive}
                  onChange={handleChange}
                />
                <span className="checkbox-custom"></span>
                Usuario Activo
              </label>
            </div>
          )}

          {error && <div className="error-message">{error}</div>}

          <div className="modal-actions">
            <button type="button" className="btn btn-outline" onClick={onClose}>
              Cancelar
            </button>
            <button type="submit" className="btn btn-primary" disabled={saving}>
              {saving ? 'Guardando...' : isEditing ? 'Actualizar' : 'Crear'}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
