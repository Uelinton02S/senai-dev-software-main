import type { Venda } from '../types/Venda'

interface Props {
  Venda: Venda[]
  loading: boolean
}

function VendaList({ Venda, loading }: Props) {
  if (loading) return <p>Carregando...</p>
  if (Venda.length === 0)
    return <p>Nenhuma venda realizada ainda.</p>

  return (
    <ul>
      {Venda.map(v => (
        <li key={v.id}>
          <strong>{v.data_venda}</strong> — {v.quantidade}
          {v.ValorTotal && <span> (ValorTotal: {v.ValorTotal})</span>}
        </li>
      ))}
    </ul>
  )
}
export default VendaList