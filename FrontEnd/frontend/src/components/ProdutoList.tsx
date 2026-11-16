
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
            <strong>
              
<div
  onClick={() => p.fotoBase64 && setProdutoModal(p)}
  style={{
    cursor: p.fotoBase64 ? 'pointer' : 'default',
    position: 'relative', overflow: 'hidden',
    borderRadius: '8px 8px 0 0'
  }}
>
  {p.fotoBase64 ? (
    <>
      <img
        src={p.fotoBase64}   // ✅ data URL direta
        alt={p.nome}
        style={{ width:'100%', height:160,
                 objectFit:'cover', display:'block' }}
      />
      <div style={{               // ícone 🔍 sobre a foto
        position:'absolute', top:8, right:8,
        background:'rgba(0,0,0,0.5)', color:'white',
        borderRadius:'50%', width:28, height:28,
        display:'flex', alignItems:'center',
        justifyContent:'center', fontSize:13
      }}>🔍</div>
    </>
  ) : (
    <div style={{                 // placeholder sem foto
      height:160, background:'#f0f0f0',
      display:'flex', flexDirection:'column',
      alignItems:'center', justifyContent:'center', color:'#bbb'
    }}>
      <span style={{ fontSize:32 }}>📷</span>
      <span style={{ fontSize:11, marginTop:4 }}>sem foto</span>
    </div>
  )}
</div>
              {p.nome}</strong>
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
