import { useState } from 'react'
import  { VendaService } from '../services/VendaService'

interface Props { VendaCriado: () => void }

export default function VendaForm({ onVendaCriado }: Props) {
  const [data_venda, setData] = useState('')
  const [quantidade, setQuantidade] = useState('')
  const [ValorTotal, setValorTotal] = useState('')
  const [loading, setLoading] = useState(false)
  const [erro, setErro] = useState<string | null>(null)

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault(); setErro(null)
    try {
      setLoading(true)
      await VendaService.criar({ data_venda, ValorTotal, quantidade })
      setData(''); setQuantidade(''); setValorTotal('')
      onVendaCriado()
    } catch { setErro('Erro ao realizar venda. Tente novamente.') }
    finally { setLoading(false) }
  }

return (
  <form onSubmit={handleSubmit}>
    <h2>Realizar Venda</h2>
    {erro && <p>{erro}</p>}

    <label htmlFor="Data">data_venda</label>
    <input id="data_venda" value={data_venda}
      onChange={e => setData(e.target.value)} required />

    <label htmlFor="quantidade">quantidade</label>
    <input id="quantidade" type="quantidade" value={quantidade}
      onChange={e => setQuantidade(e.target.value)} required />

    <label htmlFor="ValorTotal">ValorTotal</label>
    <input id="ValorTotal" value={ValorTotal}
      onChange={e => setValorTotal(e.target.value)} />

    <button disabled={loading}>
      {loading ? 'Salvando...' : 'Cadastrar'}
    </button>
  </form>
 )
}