
import { useState } from 'react'
import { useNavigate, Navigate } from 'react-router-dom'
import { useAuth } from '../context/AuthContext'
import { authService } from '../services/AuthService'
import '../assets/css/Login.css'

function LoginPage() {
  const [email, setEmail] = useState('')
  const [senha, setSenha] = useState('')
  const [loading, setLoading] = useState(false)
  const [erro, setErro] = useState<string | null>(null)

  const { login, isAuthenticated } = useAuth()
  const navigate = useNavigate()

  if (isAuthenticated) {
    return <Navigate to="/produtos" replace />
  }

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    setErro(null)

    try {
      setLoading(true)

      const resp = await authService.login({ email, senha })
      login(resp)
      navigate('/produtos')
    } catch {
      setErro('E-mail ou senha incorretos.')
    } finally {
      setLoading(false)
    }
  }

  return (
    <div className="login-container">
      <div className="login-card">
        <header className="login-header">
          <div className="login-icon">🔐</div>
          <h1>Entrar</h1>
          <p>Acesse sua conta para continuar.</p>
        </header>

        {erro && (
          <p className="login-error" role="alert">
            {erro}
          </p>
        )}

        <form className="login-form" onSubmit={handleSubmit}>
          <label htmlFor="email">E-mail</label>
          <input
            id="email"
            type="email"
            placeholder="seu@email.com"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            autoComplete="username"
            required
          />

          <label htmlFor="senha">Senha</label>
          <input
            id="senha"
            type="password"
            placeholder="Digite sua senha"
            value={senha}
            onChange={(e) => setSenha(e.target.value)}
            autoComplete="current-password"
            required
          />

          <button
            className="login-button"
            type="submit"
            disabled={loading}
          >
            {loading ? 'Entrando...' : 'Entrar →'}
          </button>
        </form>

        <footer className="login-footer">
          MinhaApi • Sistema de gerenciamento
        </footer>
      </div>
    </div>
  )
}

export default LoginPage