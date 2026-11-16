
import type { Departamento } from '../types/Departamento'

interface Props {
  Departamento: Departamento[]
  loading: boolean
}

function DepartamentoList({ Departamento, loading }: Props) {
  if (loading) return <p>Carregando departamentos...</p>

  if (Departamento.length === 0) {
    return <p>Nenhum departamento encontrado.</p>
  }

  return (
    <ul>
      {Departamento.map(d => (
        <li key={d.id}>
          <strong>{d.nome}</strong>
          {d.descricao && <span> — {d.descricao}</span>}
        </li>
      ))}
    </ul>
  )
}

export default DepartamentoList
