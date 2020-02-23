const Utils={}

//线程阻塞 sleep
Utils.sleep = function sleep(delay) {
  let start = new Date().getTime();
  while (new Date().getTime() - start < delay) {
    continue;
  }
}

export default Utils
