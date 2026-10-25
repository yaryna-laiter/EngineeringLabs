export const ITEM_COUNT = 600
export interface Item { id: number; name: string; selected: boolean }
export const createItems = (): Item[] => Array.from({ length: ITEM_COUNT }, (_, id) => ({
  id, name: `Record ${String(id + 1).padStart(3, '0')}`,
  selected: false,
}))

// Preserve object identity for every unchanged item: memo relies on this.
export const toggleItem = (items: Item[], id: number) => items.map(item =>
  item.id === id ? { ...item, selected: !item.selected } : item)

// Deliberate CPU work in the row render, identical in both implementations.
export function calculateScore(id: number, selected: boolean) {
  let score = id + (selected ? 17 : 0)
  for (let index = 0; index < 12_000; index++) score = (score * 31 + index) % 100_003
  return score
}
