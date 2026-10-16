import type { Venda } from '../types/Venda'

interface Props {
  Venda: Venda[]
  loading: boolean
}

function VendasList({ Venda, loading }: Props) {
  if (loading) return <p>Carregando...</p>
  if (Venda.length === 0)
    return <p>Nenhuma venda realizada ainda.</p>

  return (
    <ul>
      {Venda.map(v => (
        <li key={v.id}>
          <strong>{v.idproduto}</strong> — {v.idproduto}
          {v.quantidade && <span> (quantidade: {v.quantidade})</span>}
        </li>
      ))}
    </ul>
  )
}
export default VendasList