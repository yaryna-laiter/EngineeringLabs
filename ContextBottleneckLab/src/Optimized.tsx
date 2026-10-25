import { createContext, memo, useCallback, useContext, useState } from 'react'
import { createItems, toggleItem } from './data'
import type { Item } from './data'
import Row from './Row'

const ItemsContext = createContext<Item[] | null>(null)
const ToggleContext = createContext<((id: number) => void) | null>(null)
const MemoizedRow = memo(Row)

function OptimizedList() {
  const items = useContext(ItemsContext)!
  const toggle = useContext(ToggleContext)!

  // The list still renders, but unchanged item objects and the stable callback
  // let memo skip the other 599 heavy rows.
  return <ul>{items.map(item => <MemoizedRow key={item.id} item={item} toggle={toggle} />)}</ul>
}

export default function Optimized() {
  const [items, setItems] = useState(createItems)
  const toggle = useCallback((id: number) => {
    setItems(previous => toggleItem(previous, id))
  }, [])

  return <ToggleContext.Provider value={toggle}>
    <ItemsContext.Provider value={items}>
      <OptimizedList />
    </ItemsContext.Provider>
  </ToggleContext.Provider>
}
