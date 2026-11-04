import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom'
import { AuthProvider, useAuth } from './context/AuthContext'
import Sidebar from './components/SideBar'
import ProtectedRoute from './components/ProtectedRoute'
import LoginPage from './pages/LoginPage'
import ProdutosPage from './pages/ProdutosPage'
import ClientesPage from './pages/ClientesPage'
import VendasPage from './pages/vendasPage'
import DepartamentoPage from './pages/DepartamentoPage'
import DashboardPage from './pages/DashboardPage'


function Layout() {
  const { isAuthenticated } = useAuth()
  return (
    <div style={{ display: 'flex' }}>
      {isAuthenticated && <Sidebar />}
      <main style={{ flex: 1, padding: '24px' }}>
        <Routes>
          

<Route path="/" element={<Navigate to="/dashboard" replace />} />

<Route path="/dashboard" element={
  <ProtectedRoute><DashboardPage /></ProtectedRoute>
} />
          <Route path="/login" element={<LoginPage />} />
          <Route path="/" element={<Navigate to="/dashboard" replace />} />
          if (isAuthenticated) return <Navigate to="/dashboard" replace />
          <Route path="/produtos" element={
            <ProtectedRoute><ProdutosPage /></ProtectedRoute>
          } />
          <Route path="/clientes" element={
            <ProtectedRoute><ClientesPage /></ProtectedRoute>
          } />
          <Route path="/vendas" element={
            <ProtectedRoute><VendasPage /></ProtectedRoute>
          } />
          <Route path="/departamento" element={
            <ProtectedRoute><DepartamentoPage /></ProtectedRoute>
          } />
        </Routes>
      </main>
    </div>
  )
}

function App() {
  return (
    <BrowserRouter>
      <AuthProvider>
        <Layout />
      </AuthProvider>
    </BrowserRouter>
  )
}
export default App