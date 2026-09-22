import { useEffect, useState } from "react";

export function useEventSocketBuggy(value: number) {
  const [lastReceived, setLastReceived] = useState(value);

  useEffect(() => {
    const interval = setInterval(() => {
      setLastReceived(value);
    }, 1000);

    return () => clearInterval(interval);
  }, []);

  return lastReceived;
}