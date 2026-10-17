
import { NavLink } from 'react-router-dom'

const getLinkStyle = ({ isActive }: { isActive: boolean }) => ({
  display: 'block',
  padding: '20px 20px',
  color: isActive ? '#0400d4ff' : 'white',
  fontWeight: isActive ? 700 : 400,
  background: isActive ? 'rgba(241, 236, 236, 0.12)' : 'transparent',
  borderLeft: isActive ? '4px solid #c8d400' : '4px solid transparent',
  textDecoration: 'none'
})

export default function Sidebar() {
  return (
   <nav
  style={{
    width: '210px',
    background: '#2d668b',
    minHeight: '100vh',
    padding: '0',
    flexShrink: 0
  }}
>
      <div
        className="sidebar-title"
        style={{
          fontSize: '40px',
          fontWeight: 'bold',
          color: 'white',
          padding: '20px 15px',
          marginBottom: '15px'
          
        }}
      >MinhaApi</div>
      
      <NavLink to="/produtos" style={getLinkStyle}>📦 Produtos </NavLink>
      <NavLink to="/clientes" style={getLinkStyle}>👤 Clientes </NavLink>
      <NavLink to="/Vendas" style={getLinkStyle}> 👤 Vendas </NavLink>
      <NavLink to="/Departamento" style={getLinkStyle}> 🏬 Departamento </NavLink> 
      </nav>
  )
}
