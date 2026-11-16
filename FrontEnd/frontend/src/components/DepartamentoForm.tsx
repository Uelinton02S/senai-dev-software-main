
import { useState } from 'react'
import { DepartamentoService } from '../services/DepartamentoService'

interface Props {
  DepartamentoCriado: () => void
}

function DepartamentoForm({ DepartamentoCriado }: Props) {
  const [nome, setNome] = useState('')
  const [loading, setLoading] = useState(false)
  const [erro, setErro] = useState<string | null>(null)

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    setErro(null)

    try {
      setLoading(true)

      await DepartamentoService.criar({
          nome,
          email: '',
          idfornecedores: 0,
          iddepartamento: 0,
          telefone: '',
          descricao: ''
      })

      setNome('')
      DepartamentoCriado()
    } catch {
      setErro('Erro ao cadastrar departamento. Tente novamente.')
    } finally {
      setLoading(false)
    }
  }

  return (
    <form onSubmit={handleSubmit}>
      <h2>Cadastrar Departamento</h2>

      {erro && (
        <p style={{ color: 'red' }}>{erro}</p>
      )}

      <div>
        <label htmlFor="nome">Nome do departamento</label>
        <input
          id="nome"
          type="text"
          value={nome}
          onChange={e => setNome(e.target.value)}
          required
        />
      </div>

      <button type="submit" disabled={loading}>
        {loading ? 'Salvando...' : 'Cadastrar'}
      </button>
    </form>
  )
}

export default DepartamentoForm
