import { cleanup, fireEvent, render, screen } from '@testing-library/react'
import { afterEach, describe, expect, it, vi } from 'vitest'
import Baseline from './Baseline'
import Optimized from './Optimized'
import App from './App'
import * as data from './data'

afterEach(() => {
  cleanup()
  vi.restoreAllMocks()
})

describe.each([
  { name: 'baseline', Component: Baseline, expected: 600 },
  { name: 'fixed', Component: Optimized, expected: 1 },
])('$name', ({ Component, expected }) => {
  it('renders the expected rows on each checkbox update', () => {
    // Each heavy row calls this function once per render.
    const renders = vi.spyOn(data, 'calculateScore')
    const { container } = render(<Component />)
    expect(container.querySelectorAll('input')).toHaveLength(data.ITEM_COUNT)
    expect(renders).toHaveBeenCalledTimes(data.ITEM_COUNT)

    for (const id of [0, 357, 599, 0]) {
      renders.mockClear()
      const checkbox = screen.getByLabelText('Record ' + String(id + 1).padStart(3, '0')) as HTMLInputElement
      const previous = checkbox.checked
      fireEvent.click(checkbox)
      expect(checkbox.checked).toBe(!previous)
      expect(renders).toHaveBeenCalledTimes(expected)
      if (expected === 1) expect(renders).toHaveBeenCalledWith(id, !previous)
    }
    expect((screen.getByLabelText('Record 001') as HTMLInputElement).checked).toBe(false)
    expect((screen.getByLabelText('Record 358') as HTMLInputElement).checked).toBe(true)
    expect((screen.getByLabelText('Record 002') as HTMLInputElement).checked).toBe(false)
  }, 20_000)
})

it('switches implementations with fresh selection state', () => {
  render(<App />)
  fireEvent.click(screen.getByLabelText('Record 001'))
  expect(screen.getByTestId('render-count').textContent).toBe('600')
  fireEvent.click(screen.getByRole('button', { name: 'Optimized' }))
  expect((screen.getByLabelText('Record 001') as HTMLInputElement).checked).toBe(false)
  expect(screen.getByTestId('render-count').textContent).toBe('600')
  fireEvent.click(screen.getByLabelText('Record 001'))
  expect((screen.getByLabelText('Record 001') as HTMLInputElement).checked).toBe(true)
  expect(screen.getByTestId('render-count').textContent).toBe('1')
})
