import { useState, useEffect } from 'react'
import { productService } from './services/productService'
import { categoryService } from './services/categoryService'
import type { Product, Category } from './types'
import './App.css'

function App() {
  const [products, setProducts] = useState<Product[]>([])
  const [categories, setCategories] = useState<Category[]>([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)

  useEffect(() => {
    const loadData = async () => {
      try {
        const [prods, cats] = await Promise.all([
          productService.getActive(),
          categoryService.getAll(),
        ])
        setProducts(prods)
        setCategories(cats)
      } catch (err) {
        setError('Falha ao carregar dados. Verifique se a API está em execução.')
        console.error(err)
      } finally {
        setLoading(false)
      }
    }

    loadData()
  }, [])

  return (
    <div className="app">
      <header>
        <h1>Catalog Retail</h1>
        <p>Gerencie seus produtos e categorias</p>
      </header>

      <main>
        {loading && <p>Carregando...</p>}
        {error && <p className="error">{error}</p>}

        {!loading && !error && (
          <>
            <section>
              <h2>Categorias ({categories.length})</h2>
              <ul>
                {categories.map((cat) => (
                  <li key={cat.id}>{cat.name}</li>
                ))}
              </ul>
            </section>

            <section>
              <h2>Produtos Ativos ({products.length})</h2>
              <ul>
                {products.map((prod) => (
                  <li key={prod.id}>
                    <strong>{prod.name}</strong> — R$ {prod.price.toFixed(2)} — Estoque: {prod.stock}
                  </li>
                ))}
              </ul>
            </section>
          </>
        )}
      </main>
    </div>
  )
}

export default App
