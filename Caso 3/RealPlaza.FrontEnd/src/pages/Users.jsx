import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../context/AuthContext";
import {
  getUsers,
  createUser,
  updateUser,
  deleteUser,
} from "../api/userService";
import UserForm from "../components/UserForm";

export default function Users() {
  const [users, setUsers] = useState([]);
  const [loading, setLoading] = useState(true);
  const [showModal, setShowModal] = useState(false);
  const [editingUser, setEditingUser] = useState(null);
  const [error, setError] = useState("");
  const { user, logout } = useAuth();
  const navigate = useNavigate();

  const fetchUsers = async () => {
    try {
      setLoading(true);
      const res = await getUsers();
      setUsers(res.data);
    } catch (err) {
      setError("Error al cargar usuarios.");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchUsers();
  }, []);

  const handleCreate = () => {
    setEditingUser(null);
    setShowModal(true);
  };

  const handleEdit = (u) => {
    setEditingUser(u);
    setShowModal(true);
  };

  const handleDelete = async (id) => {
    if (!window.confirm("¿Está seguro de eliminar este usuario?")) return;
    try {
      await deleteUser(id);
      await fetchUsers();
    } catch (err) {
      setError("Error al eliminar usuario.");
    }
  };

  const handleSave = async (formData) => {
    try {
      if (editingUser) {
        await updateUser(editingUser.id, {
          username: formData.username,
          email: formData.email,
          birthDate: formData.birthDate,
          isActive: formData.isActive,
        });
      } else {
        await createUser({
          username: formData.username,
          passwordHash: formData.passwordHash,
          email: formData.email,
          birthDate: formData.birthDate,
        });
      }
      setShowModal(false);
      setEditingUser(null);
      await fetchUsers();
    } catch (err) {
      if (err.response?.status === 500) {
        throw new Error("El nombre de usuario ya existe o datos inválidos.");
      }
      throw new Error("Error al guardar usuario.");
    }
  };

  const handleLogout = () => {
    logout();
    navigate("/login");
  };

  const formatDate = (dateStr) => {
    if (!dateStr) return "-";
    const parts = dateStr.split("T")[0].split("-");
    return `${parts[2]}/${parts[1]}/${parts[0]}`;
  };

  return (
    <div className="users-container">
      <header className="app-header">
        <div className="header-left">
          <h1>Mantenimiento de Usuarios</h1>
        </div>
        <div className="header-right">
          <span className="user-info">
            Bienvenido, <strong>{user?.username}</strong>
          </span>
          <button className="btn btn-outline" onClick={handleLogout}>
            Cerrar Sesión
          </button>
        </div>
      </header>

      <main className="main-content">
        {error && (
          <div className="error-message" style={{ marginBottom: "1rem" }}>
            {error}
            <button className="btn-close-error" onClick={() => setError("")}>
              ×
            </button>
          </div>
        )}

        <div className="table-header">
          <h2>Lista de Usuarios</h2>
          <button className="btn btn-primary" onClick={handleCreate}>
            + Nuevo Usuario
          </button>
        </div>

        {loading ? (
          <div className="loading">Cargando...</div>
        ) : users.length === 0 ? (
          <div className="empty-state">No hay usuarios registrados.</div>
        ) : (
          <div className="table-wrapper">
            <table className="data-table">
              <thead>
                <tr>
                  <th>ID</th>
                  <th>Usuario</th>
                  <th>Email</th>
                  <th>Fecha Nacimiento</th>
                  <th>Estado</th>
                  <th>Acciones</th>
                </tr>
              </thead>
              <tbody>
                {users.map((u) => (
                  <tr key={u.id}>
                    <td>{u.id}</td>
                    <td>{u.username}</td>
                    <td>{u.email}</td>
                    <td>{formatDate(u.birthDate)}</td>
                    <td>
                      <span
                        className={`badge ${u.isActive ? "badge-active" : "badge-inactive"}`}
                      >
                        {u.isActive ? "Activo" : "Inactivo"}
                      </span>
                    </td>
                    <td>
                      <div className="action-buttons">
                        <button
                          className="btn btn-sm btn-edit"
                          onClick={() => handleEdit(u)}
                        >
                          Editar
                        </button>
                        <button
                          className="btn btn-sm btn-delete"
                          onClick={() => handleDelete(u.id)}
                        >
                          Eliminar
                        </button>
                      </div>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
      </main>

      {showModal && (
        <UserForm
          user={editingUser}
          onSave={handleSave}
          onClose={() => {
            setShowModal(false);
            setEditingUser(null);
          }}
        />
      )}
    </div>
  );
}
