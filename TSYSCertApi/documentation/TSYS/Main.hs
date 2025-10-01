{-# LANGUAGE OverloadedStrings #-}

module Main where

import qualified Data.ByteString.Lazy as BSL
import Data.Bits (xor)
import Data.Foldable (foldr)

import Network.Wreq
import Control.Lens ((.~) -- Think of it as 'set', as in "X is set to a value of Y" in x .~ y
                    ,(^.) -- Think of it as 'view', as in "In structure X, view the value of Y" in x ^. y
                    ,(&))

main = do
  -- build out headers required. Set defaults of library as base since they don't matter, plus the required Content-Type
  let options = defaults & header "Content-Type" .~ ["x-Visa-II/x-auth"]
  -- send a post request
  response <- postWith options url finalMessage
  -- Print dump the response
  BSL.putStr (response ^. responseBody)
  BSL.putStr "\n"

  -- builds the final message comprised of stx, the message, etx and lrc.
finalMessage = stx `BSL.append` message `BSL.append` etx `BSL.append` lrc

-- Sierra/Summit message to send. Use protocol 4 (multithreaded connection).
--message = "D4.999995777777777777"
message = "H4.TSH950<SGREQ><A1>999999999999999</A1></SGREQ>"

-- TSYS URL to connect to
url = "https://ssltest.tsysacquiring.net/scripts/gateway.dll?transact"

-- Special ASCII Characters; Numbers represent ASCII characters in decimal base
-- BSL.singleton turns a single 8bit number into a bytestring
stx = BSL.singleton 2
etx = BSL.singleton 3
etb = BSL.singleton 23
-- Field seperator
fs  = BSL.singleton 28
-- Group seperator
gs  = BSL.singleton 29
-- LRC is a bitwise XOR between all characters of the message plus ETX
lrc = BSL.singleton (foldr xor 0 word8List)
      where word8List = BSL.unpack (message `BSL.append` etx)

