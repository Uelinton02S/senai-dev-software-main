
import { useState } from 'react'
import type { Produto } from '../types/Produto'
import ModalFoto from '../components/Produtos/ModalFoto'

interface Props {
  produtos: Produto[]
  loading: boolean
}

function ProdutoList({ produtos, loading }: Props) {

  const [produtoModal, setProdutoModal] = useState<Produto | null>(null)

  if (loading)
    return <p>Carregando...</p>

  if (produtos.length === 0)
    return <p>Nenhum produto cadastrado ainda.</p>

  return (
    <>
      <ul>
        {produtos.map(p => (
          <li key={p.id}>
            <strong>{p.nome}</strong>
            {' — '}
            R$ {p.preco.toFixed(2)}

            <button onClick={() => setProdutoModal(p)}>
              Ver foto
            </button>
          </li>
        ))}
      </ul>

      {produtoModal && (
        <ModalFoto
          produto={produtoModal}
          onClose={() => setProdutoModal(null)}
        />
      )}
    </>
  )
}

export default ProdutoList
