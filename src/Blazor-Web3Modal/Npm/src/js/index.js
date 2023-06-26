import { EthereumClient, w3mConnectors, w3mProvider} from "@web3modal/ethereum";
import { Web3Modal } from "@web3modal/html";
import { configureChains, createClient, prepareSendTransaction, sendTransaction, signMessage, signTypedData, fetchBlockNumber,
         fetchEnsAddress, fetchFeeData, fetchTransaction, fetchToken, watchBlockNumber } from "@wagmi/core";
import * as chains from "@wagmi/core/chains";
import { BigNumber } from "@ethersproject/bignumber";

let modal;
let ethereumClient;
let configured = false;
let unwatchBlockNumber;

function convertChains(selectedChains) {
  let arr = [];

  selectedChains.forEach((chain) => {
    switch (chain) {
      case 0:
        arr.push(chains.arbitrum);
        break;
      case 1:
        arr.push(chains.arbitrumGoerli);
        break;
      case 2:
        arr.push(chains.aurora);
        break;
      case 3:
        arr.push(chains.auroraTestnet);
        break;
      case 4:
        arr.push(chains.avalanche);
        break;
      case 5:
        arr.push(chains.avalancheFuji);
        break;
      case 6:
        arr.push(chains.baseGoerli);
        break;
      case 7:
        arr.push(chains.boba);
        break;
      case 8:
        arr.push(chains.bronos);
        break;
      case 9:
        arr.push(chains.bronosTestnet);
        break;
      case 10:
        arr.push(chains.bsc);
        break;
      case 11:
        arr.push(chains.bscTestnet);
        break;
      case 12:
        arr.push(chains.canto);
        break;
      case 13:
        arr.push(chains.celo);
        break;
      case 14:
        arr.push(chains.celoAlfajores);
        break;
      case 15:
        arr.push(chains.cronos);
        break;
      case 16:
        arr.push(chains.crossbell);
        break;
      case 17:
        arr.push(chains.dfk);
        break;
      case 18:
        arr.push(chains.dogechain);
        break;
      case 19:
        arr.push(chains.evmos);
        break;
      case 20:
        arr.push(chains.evmosTestnet);
        break;
      case 21:
        arr.push(chains.fantom);
        break;
      case 22:
        arr.push(chains.fantomTestnet);
        break;
      case 23:
        arr.push(chains.filecoin);
        break;
      case 24:
        arr.push(chains.filecoinHyperspace);
        break;
      case 25:
        arr.push(chains.foundry);
        break;
      case 26:
        arr.push(chains.iotex);
        break;
      case 27:
        arr.push(chains.iotexTestnet);
        break;
      case 28:
        arr.push(chains.goerli);
        break;
      case 29:
        arr.push(chains.gnosis);
        break;
      case 30:
        arr.push(chains.gnosisChiado);
        break;
      case 31:
        arr.push(chains.hardhat);
        break;
      case 32:
        arr.push(chains.harmonyOne);
        break;
      case 33:
        arr.push(chains.klaytn);
        break;
      case 34:
        arr.push(chains.localhost);
        break;
      case 35:
        arr.push(chains.mainnet);
        break;
      case 36:
        arr.push(chains.metis);
        break;
      case 37:
        arr.push(chains.metisGoerli);
        break;
      case 38:
        arr.push(chains.moonbaseAlpha);
        break;
      case 39:
        arr.push(chains.moonbeam);
        break;
      case 40:
        arr.push(chains.moonriver);
        break;
      case 41:
        arr.push(chains.okc);
        break;
      case 42:
        arr.push(chains.optimism);
        break;
      case 43:
        arr.push(chains.optimismGoerli);
        break;
      case 44:
        arr.push(chains.polygon);
        break;
      case 45:
        arr.push(chains.polygonMumbai);
        break;
      case 46:
        arr.push(chains.polygonZkEvmTestnet);
        break;
      case 47:
        arr.push(chains.scrollTestnet);
        break;
      case 48:
        arr.push(chains.sepolia);
        break;
      case 49:
        arr.push(chains.skaleBlockBrawlers);
        break;
      case 50:
        arr.push(chains.skaleCalypso);
        break;
      case 51:
        arr.push(chains.skaleCalypsoTestnet);
        break;
      case 52:
        arr.push(chains.skaleChaosTestnet);
        break;
      case 53:
        arr.push(chains.skaleCryptoBlades);
        break;
      case 54:
        arr.push(chains.skaleCryptoColosseum);
        break;
      case 55:
        arr.push(chains.skaleEuropa);
        break;
      case 56:
        arr.push(chains.skaleEuropaTestnet);
        break;
      case 57:
        arr.push(chains.skaleExorde);
        break;
      case 58:
        arr.push(chains.skaleHumanProtocol);
        break;
      case 59:
        arr.push(chains.skaleNebula);
        break;
      case 60:
        arr.push(chains.skaleNebulaTestnet);
        break;
      case 61:
        arr.push(chains.skaleRazor);
        break;
      case 62:
        arr.push(chains.skaleTitan);
        break;
      case 63:
        arr.push(chains.skaleTitanTestnet);
        break;
      case 64:
        arr.push(chains.shardeumSphinx);
        break;
      case 65:
        arr.push(chains.taraxa);
        break;
      case 66:
        arr.push(chains.taraxaTestnet);
        break;
      case 67:
        arr.push(chains.telos);
        break;
      case 68:
        arr.push(chains.telosTestnet);
        break;
      case 69:
        arr.push(chains.wanchain);
        break;
      case 70:
        arr.push(chains.wanchainTestnet);
        break;
      case 71:
        arr.push(chains.zhejiang);
        break;
      case 72:
        arr.push(chains.zkSync);
        break;
      case 73:
        arr.push(chains.zkSyncTestnet);
        break;
      default:
        throw `Unrecognised chain: ${chain}`;
    }
  });

  return arr;
}

function onAccountChangedReplacer(key, value) {
  if(key == "connector") {
    return undefined;
  }
  return value;
}

function getErrorResponse(e) {
  let response = {
    result: null,
    error: e.reason ?? e.message ?? e,
    success: false
  }
  return JSON.stringify(response);
}

function getSuccessResponse(result) {
  let response = {
    result: result,
    error: null,
    success: true
  };
  return JSON.stringify(response);
}

function getSuccessResponseWithReplacer(result, replacer) {
  let response = {
    result: result,
    error: null,
    success: true
  };
  return JSON.stringify(response, replacer);

}

export async function configure(options, dotNetInterop) {
  if(configured){
    return;
  }
  let {projectId, selectedChains, autoConnect, themeOptions, mobileWallets, desktopWallets, walletImages,
       chainImages, tokenImages, explorerAllowList, explorerDenyList, termsOfServiceUrl, privacyPolicyUrl,
       enableNetworkView, enableAccountView, enableExplorer} = JSON.parse(options);

  const convertedChains = convertChains(selectedChains);
  const { provider } = configureChains(convertedChains, [w3mProvider({projectId})]);

  const wagmiClient = createClient({
    autoConnect: autoConnect,
    connectors: w3mConnectors({projectId, version: 2, convertedChains}),
    provider
  });

  ethereumClient = new EthereumClient(wagmiClient, convertedChains);
  modal = new Web3Modal({
    projectId,
    themeMode: themeOptions.themeMode == 0 ? 'light' : 'dark',
    themeVariables: themeOptions.themeVariables,
    mobileWallets: mobileWallets,
    desktopWallets: desktopWallets,
    walletImages: walletImages,
    chainImages: chainImages,
    tokenImages: tokenImages,
    explorerAllowList: explorerAllowList,
    explorerDenyList: explorerDenyList,
    termsOfServiceUrl: termsOfServiceUrl,
    privacyPolicyUrl: privacyPolicyUrl,
    enableNetworkView: enableNetworkView,
    enableAccountView: enableAccountView,
    enableExplorer: enableExplorer
  }, ethereumClient);

  ethereumClient.watchAccount((account) => {
    dotNetInterop.invokeMethodAsync('OnAccountChanged', JSON.stringify(account, onAccountChangedReplacer));
  });
  ethereumClient.watchNetwork((network) => {
    dotNetInterop.invokeMethodAsync('OnNetworkChanged', JSON.stringify(network.chain));
  });
  
  configured = true;
}

export async function openModal() {
  if(!configured){
    throw "Attempting to open modal before we have configured.";
  }
  await modal.openModal();
}

export async function closeModal() {
  if(!configured){
    throw "Attempting to close modal before we have configured.";
  }
  await modal.closeModal();
}

export function setTheme(themeMode, themeVariables) {
  if(!configured) {
    throw "Attempting to set theme before we have configured.";
  }
  modal.setTheme({
    themeMode: themeMode,
    themeVariables: themeVariables
  });
}

export async function disconnect() {
  if(!configured) {
    throw "Attempting to disconnect before we have configured.";
  }
  await ethereumClient.disconnect();
}

export function getAccount() {
  if(!configured) {
    throw "Attempting to get account before we have configured.";
  }
  
  try {
    let result = ethereumClient.getAccount();
    return getSuccessResponseWithReplacer(result, onAccountChangedReplacer);
  }
  catch(e) {
    return getErrorResponse(e);
  }
}

export function getNetwork() {
  if(!configured) {
    throw "Attempting to get network before we have configured.";
  }
  
  try {
    let network = ethereumClient.getNetwork();
    return getSuccessResponse(network.chain);
  }
  catch(e) {
    return getErrorResponse(e);
  }
}

export async function switchNetwork(chainId) {
  if(!configured) {
    throw "Attempting to switch network before we have configured.";
  }
  
  try {
    let result = await ethereumClient.switchNetwork({chainId});
    return getSuccessResponse(result);
  }
  catch(e) {
    return getErrorResponse(e);
  }
}

export async function trySendTransaction(input, dotNetInterop) {
  if(!configured) {
    throw "Attempting to send transaction before we have configured.";
  }
  let parsedTransaction = JSON.parse(input);
  delete parsedTransaction.gas;

  try {
    const config = await prepareSendTransaction({
      request: parsedTransaction
    });
    
    if(config.request.gasLimit.gt(BigNumber.from("21000"))) {
      config.request.gasLimit = config.request.gasLimit.div(4).mul(5);
    }

    const { hash, wait } = await sendTransaction(config);

    setTimeout(async () => {
      try {
        let result = await wait();

        dotNetInterop.invokeMethodAsync("OnTransactionComplete", JSON.stringify(result));
      }
      catch(e) {
        console.log(e);
      }
    }, 0);

    return getSuccessResponse(hash);
  }
  catch(e) {
    return getErrorResponse(e);
  }
}

export async function trySignMessage(message) {
  if(!configured) {
    throw "Attempting to sign message before we have configured.";
  }

  try {
    const result = await signMessage({
      message: message
    });

    return getSuccessResponse(result);
  }
  catch(e) {
    return getErrorResponse(e);
  }
}

export async function trySignTypedData(domain, types, value) {
  if(!configured) {
    throw "Attempting to sign typed data before we have configured.";
  }

  try {
    let parsedDomain = JSON.parse(domain);
    let parsedTypes = JSON.parse(types);
    let parsedValue = JSON.parse(value);

    const result = await signTypedData({
      domain: parsedDomain,
      types: parsedTypes.types,
      value: parsedValue
    });

    return getSuccessResponse(result);
  }
  catch(e) {
    return getErrorResponse(e);
  }
}

export async function getBalance(address, chainId, tokenAddress) {
  if(!configured) {
    throw "Attempting to get balance before we have configured.";
  }

  try {
    let result = await ethereumClient.fetchBalance({address, chainId, token: tokenAddress});
    
    return getSuccessResponse(result);
  }
  catch(e) {
    return getErrorResponse(e);
  }
}

export async function getEnsAvatar(address, chainId){
  if(!configured) {
    throw "Attempting to get ens avatar before we have configured.";
  }
  
  try {
    let result = await ethereumClient.fetchEnsAvatar({address, chainId});
    
    return getSuccessResponse(result);
  }
  catch(e) {
    return getErrorResponse(e);
  }
}

export async function getEnsName(address, chainId) {
  if(!configured) {
    throw "Attempting to get ens name before we have configured.";
  }
  
  try {
    let result = await ethereumClient.fetchEnsName({address, chainId});
    
    return getSuccessResponse(result);
  }
  catch(e) {
    return getErrorResponse(e);
  }
}

export async function getEnsAddress(name, chainId) {
  if(!configured) {
    throw "Attempting to get ens address before we have configured.";
  }
  
  try {
    let result = await fetchEnsAddress({chainId, name});
    
    return getSuccessResponse(result);
  }
  catch(e) {
    return getErrorResponse(e);
  }
}

export async function getFeeData(chainId) {
  if(!configured) {
    throw "Attempting to get fee data before we have configured.";
  }
  
  try {
    let result = await fetchFeeData({formatUnits: 'gwei', chainId});
    
    return getSuccessResponse(result.formatted);
  }
  catch(e) {
    return getErrorResponse(e);
  }
}

export async function getTransaction(hash, chainId) {
  if(!configured) {
    throw "Attempting to get transaction before we have configured.";
  }
  
  try {
    let result = await fetchTransaction({chainId, hash});
    
    return getSuccessResponse(result);
  }
  catch(e) {
    return getErrorResponse(e);
  }
}

export async function getToken(address, chainId) {
  if(!configured) {
    throw "Attempting to get token before we have configured.";
  }
  
  try {
    let result = await fetchToken({address, chainId});
    result.totalSupply = result.totalSupply.value;
    
    return getSuccessResponse(result);
  }
  catch(e) {
    return getErrorResponse(e);
  }
}

export async function getBlockNumber(chainId){
  if(!configured) {
    throw "Attempting to get block number before we have configured.";
  }
  
  try {
    let result = await fetchBlockNumber({chainId});
    
    return getSuccessResponse(result);
  }
  catch(e) {
    return getErrorResponse(e);
  }
}

export async function startWatchingBlockNumber(chainId, dotNetInterop) {
  if(!configured) {
    throw "Attempting to start watching block number before we have configured.";
  }
  
  if(unwatchBlockNumber == undefined) {
    unwatchBlockNumber = watchBlockNumber({chainId: chainId, listen: true},
      (blockNumber) => {
        dotNetInterop.invokeMethodAsync('OnBlockNumber', JSON.stringify(blockNumber));
      });
  }
}

export async function stopWatchingBlockNumber() {
  if(!configured) {
    throw "Attempting to stop watching block number before we have configured.";
  }

  if(unwatchBlockNumber != undefined) {
    unwatchBlockNumber();
    unwatchBlockNumber = undefined;
  }
}