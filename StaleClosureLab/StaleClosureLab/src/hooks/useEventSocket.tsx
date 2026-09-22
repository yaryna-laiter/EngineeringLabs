import { useEffect, useRef, useState } from "react";

export function useEventSocket(value: number) {
  const valueRef = useRef(value);
  const [lastReceived, setLastReceived] = useState(value);

  valueRef.current = value;

  useEffect(() => {
    const interval = setInterval(() => {
      setLastReceived(valueRef.current);
    }, 1000);

    return () => clearInterval(interval);
  }, []);

  return lastReceived;
}